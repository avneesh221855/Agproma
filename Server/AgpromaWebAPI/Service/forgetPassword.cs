using AgProMa.Repository;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForgetPassword.service
{
    public interface IforgetPassword
    {
        bool EmailForResetPassword(string email);
    }

    public class forgetPassword : IforgetPassword
    {
        private ISignUpRepository _repo;       
        private readonly IConfiguration _config;
        public forgetPassword(IConfiguration config,ISignUpRepository repo)

        {
            _repo = repo;
            _config = config;

        }
        public bool EmailForResetPassword(string email)
        {
            List<User> master = _repo.GetAllDetails();
            foreach (User mast in master)
            {
                if (mast.Email == email)
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(_config["EmailConfig:Title"], _config["EmailConfig:FromEmail"])); //mail title and mail from(Email)
                    message.To.Add(new MailboxAddress(email)); // Mail to(Email)
                    message.Subject = _config["EmailConfig:SubjectForEmailReset"]; //mail subject
                    var bodyBuilder = new BodyBuilder();
                    //body of the mail
                    bodyBuilder.HtmlBody = "Click here to reset your password-  http://localhost:4200/app-register-user-with-new-password/" + 1;
                    message.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        //required field for email
                        client.Connect(_config["EmailConfig:Domain"], 587, false);
                        client.Authenticate(_config["EmailConfig:FromEmail"], _config["EmailConfig:Password"]);
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    return true;
                }
            }
            return false;
        }
    }
}

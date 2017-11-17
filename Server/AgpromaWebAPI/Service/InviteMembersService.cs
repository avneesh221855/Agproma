using AgProMa.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using AgpromaWebAPI.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AgpromaWebAPI.Service
{
    public interface IinviteMembersService
    {
        int EmailForInvitation(InvitePeople people);
        List<InviteExistingMember> GetMemberName(int id);
    }

    public class InviteMembersService : IinviteMembersService
    {
        private IConfiguration _config;
        private IInviteRepository _repository;
        private ISignUpService _signup;
        private IProjectmemberservice _Projectmembers;
        public InviteMembersService(IInviteRepository repository,IConfiguration config, ISignUpService signup, IProjectmemberservice Projectmembers )
        { 
           
            _repository = repository;
            _config = config;
            _signup = signup;
            _Projectmembers = Projectmembers;
        }
        



        public int EmailForInvitation(InvitePeople people)
        {
            int user = _signup.GetId(people.Email);
            List<Projectmembers> member = _Projectmembers.getMemberDetails(user);
            foreach (var memberdetail in member)
            {
                if (memberdetail.ProjectId.ToString() == people.ProjectId)
                {
                    return 0;
                }
            }
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(_config["EmailConfig:Title"], _config["EmailConfig:FromEmail"])); //mail title and mail from(Email)
                    message.To.Add(new MailboxAddress(people.Email)); //mail to(client)
                    message.Subject = _config["EmailConfig:SubjectForInvitation"]; //mail subject
                    var bodyBuilder = new BodyBuilder();
            //body of the mail
            if (user!=0 )
            {
                bodyBuilder.HtmlBody = "Click here to join project-  http://localhost:4200/app-signup/" + people.ProjectId; //link sent in mail
            }
            else
            {
                bodyBuilder.HtmlBody = "Click here to join project-  http://localhost:4200/app-register/" + people.ProjectId; //link sent in mail
            }
                message.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
			//required field for email
                        client.Connect(_config["EmailConfig:Domain"], 587, false);
                        client.Authenticate(_config["EmailConfig:FromEmail"], _config["EmailConfig:Password"]);
                        client.Send(message);
                        client.Disconnect(true);
                    }
            return 1;
        }

        public List<InviteExistingMember> GetMemberName(int id)
        {
            List<InviteExistingMember> myMember = new List<InviteExistingMember>();
            List<Projectmembers> member = _repository.GetMemberName();
            foreach (Projectmembers pm in member)
            {
                if (pm.ProjectId == id)
                {
                    User master = _repository.AllData(pm.MemberId);
                    InviteExistingMember am = new InviteExistingMember();
                    am.ProjectId = master.Id;
                    am.Email = master.Email;
                    am.MemberName = master.FirstName + " " + master.LastName;
                    myMember.Add(am);
                }
            }

            return myMember;
        }
    }
}

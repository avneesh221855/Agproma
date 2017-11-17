using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ForgetPassword.service;

namespace ForgetPassword.Controllers
{
    [Produces("application/json")]
    [Route("api/ForgetPassword")]
    public class ForgetPasswordController : Controller
    {

        private IforgetPassword _service;

        public ForgetPasswordController(IforgetPassword service)
        {
            _service = service;
        }


        // PUT: api/ForgetPassword/5
        [HttpPost]
        public IActionResult post([FromQuery]string email)
        {
            try
            {
                bool result =  _service.EmailForResetPassword(email); //mail for forget password
                return Ok(result);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.StackTrace);//handling bad request
                return BadRequest();
            }
            catch (Exception )
            {
                return StatusCode(500); //internal server error
            }
        }
        
    }
}

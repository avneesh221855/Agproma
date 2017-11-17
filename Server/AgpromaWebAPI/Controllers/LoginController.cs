using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgProMa.Services;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Viewmodel;
using Microsoft.AspNetCore.Authorization;

namespace AgProMa.Controllers
{
   
    [Produces("application/json")]
    public class LoginController : Controller
    {
        private ISignUpService _context;
        public LoginController(ISignUpService context)
        {

            _context = context;
        }
       // GET api/values
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            //exception handling
            try
            {
                List<User> list = _context.GetAllDetails();
                if (list.Count != 0)
                {
                    return Ok(list);
                }
                else if(list.Count == 0)
                {
                    return Ok("no data found");
                }
                else
                {
                    return StatusCode(404);
                }

            }
            catch
            {

                return StatusCode(500);
            }
        }

       
        [HttpGet]
        [Route("api/[controller]/{email}")]
        //this method updates the user details
        public int Get(string email)
        {
            return _context.GetId(email);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/check")]
        //this method get the details of a particular user
        public IActionResult Check([FromBody]IdPassword modal)
        {
            return Ok(_context.Check(modal));
        }

        [HttpPost]
        [Route("api/[controller]/SetLogOut/{id}")]
        //this method get the details of a particular user
        public IActionResult SetLogOut(int id)
        {
            string response = _context.logOut(id);
            return Ok(response);
        }


        // POST api/values

        [HttpPost]
        [Route("api/[controller]")]
        //this method adds a user details
        public IActionResult PostUserDetails([FromBody]User user)
        {
           string str =  _context.Add_User(user);          
           return Ok(str);
        }

        // PUT api/values/5
        [HttpPut]
        [Route("api/[controller]/{id}")]
        //this method updates the user details
        public IActionResult Put(string emailid, [FromBody]User user)
        {
            //exceptional handling
            try
            {
                _context.Update(emailid, user);
                return Ok("success");
            }
            catch
            {
                return Ok("internal server error");

            }
        }
        //get details for view profile
        [HttpGet]
        [Route("api/[controller]/Details/{id}")]
        //this method updates the user details
        public IActionResult Details(int id)
        {
            try
            {
                return Ok(_context.GetById(id));
            }
            catch
            {
                return BadRequest("Bad Request");
            }
            }
        [HttpPut]
        [Route("api/[controller]/UpdatePassword/{id}")]
        //update user password
        public void UpdatePassword(int id,[FromBody]User user)
        {
            _context.UpdatePassword(id, user);
        }
    }
}

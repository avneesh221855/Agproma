using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgProMa.Controllers
{
    [Produces("application/json")]
    public class ProjectmembersController : Controller
    {
        private IProjectmemberservice _context;
        public ProjectmembersController(IProjectmemberservice context)
        {

            _context = context;
        }
        // GET api/values
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            return Ok("oidsjicd");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public void PostMemberDetails([FromBody]Projectmembers user)
        {
            _context.Add_MemberDetails(user);
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgpromaWebAPI.Service;
using AgpromaWebAPI.model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgpromaWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SprintController : Controller
    {
        public ISprintService _service;
        public SprintController(ISprintService service)
        {
            _service = service;
        }

        // GET: api/values
        //it will get all the sprint backlogs for current project id.
        [HttpGet("{id}")]
        public List<Sprint> GetAll(int id)
        {
            return _service.GetAll(id);
        }

        // POST api/values
        //it will add a new sprint.
        [HttpPost]
        public void Post([FromBody]Sprint sprint)
        {
            _service.Add(sprint);
        }

        // PUT api/values/5
        //it will update the current sprint details.
        [HttpPut("{id}")]
        public void Put(int sprintId,[FromBody] UserStory sprint)
        {
            _service.Update(sprintId,sprint);
        }

        // DELETE api/values/5
        //it will delete the sprint.
        [HttpDelete("{id}")]
        public void Delete(int sprintId)
        {
            _service.Delete(sprintId);
        }
    }
}

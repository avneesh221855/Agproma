using Microsoft.AspNetCore.Mvc;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/ReleasePlan")]
    public class ReleasePlansanController : Controller
    {
        IReleasePlansService _ReleasePlansanService;
        public ReleasePlansanController(IReleasePlansService ReleasePlansanService)
        {
            _ReleasePlansanService = ReleasePlansanService;
        }

        // GET api/Release
        [HttpGet("GetRelease/{id}")]
        public List<ReleasePlan> GetRelease(int id)
        {
            List<ReleasePlan> data = _ReleasePlansanService.GetAllReleasePlans(id);
            return data;
        }
        //GET api/Sprint
        [HttpGet("GetSprint/{id}")]
        public List<Sprint> GetSprint(int id)
        {
            List<Sprint> data = _ReleasePlansanService.GetAllSprints(id);
            return data;
        }

        // POST api/Release
        [HttpPost]
        public void Post([FromBody]ReleasePlan ReleasePlan)
        {
            _ReleasePlansanService.AddRelease(ReleasePlan);
        }
    }
}

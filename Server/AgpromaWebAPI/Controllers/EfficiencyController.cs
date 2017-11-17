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
    public class EfficiencyController : Controller
    {
        public IEfficiencyService _service;
        public EfficiencyController(IEfficiencyService service)
        {
            _service = service;
        }

        // GET api/values/5
        [HttpGet()]
        //get efficiency for a user.
        [Route("api/[controller]/GetEfficiencyForUser/{userId}")]
        public float Get(int userId)
        {
           return _service.GetEfficiencyForUser(userId);
        }

        // GET api/values/5
        [HttpGet()]
        [Route("api/[controller]/GetEfficiencyForUserByProjectId/{userId}")]
        //get efficiency for a user by project id.
        public float GetEfficiencyForUserByProjectId(int userId)
        {
            return _service.GetEfficiencyForUserByProjectId(userId);
        }
    }
}

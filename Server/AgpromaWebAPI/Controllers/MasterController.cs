using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgpromaWebAPI.Service;
using AgpromaWebAPI.model;

namespace AgpromaWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Master")]
    public class MasterController : Controller
    {
        private IMasterService _service;

        public MasterController(IMasterService service)
        {
            _service = service;
        }

        // GET: api/Master
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Master/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
           User master =   _service.getUserDetailsService(id);
            return master;
        }
        
        // POST: api/Master
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Master/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]User userDetail)
        {
            _service.updateDetails(id,userDetail);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

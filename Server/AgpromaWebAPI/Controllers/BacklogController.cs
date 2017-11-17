using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgpromaWebAPI.Service;
using AgpromaWebAPI.model;
using Microsoft.AspNetCore.Authorization;

namespace AgpromaWebAPI.Controllers
{
    // [Produces("application/json")]

     [Route("api/[Controller]")]
    public class BacklogController:Controller {
        private IBacklogServices _service;

        public BacklogController(IBacklogServices service)
        {
            _service = service;

        }
        // GET: api/backlog
        [HttpGet]
        [Route("{id}")]
        public IEnumerable<UserStory> Get(int id)
        {
            List<UserStory> data = _service.GetAll(id);
                return data;
           
        }

        // GET: api/Master/5
        //    [HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Master
        [HttpPost]
         public void Post([FromBody]UserStory backlog)
    {
            _service.Add(backlog);

    }
      
        [HttpPut("{id}")]
        public void put(int id, [FromBody]UserStory value)
        {
            _service.Update(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
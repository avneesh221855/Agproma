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
    [Route("api/[controller]")]
    public class EpicController : Controller
    {
        private IEpicServices _service;

        public EpicController(IEpicServices service)
        {
            _service = service;

        }
        // GET: api/backlog
        [HttpGet("{id}")]
        public IEnumerable<EpicMaster> Get(int id)
        {
            List<EpicMaster> data = _service.GetAll(id);
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
        public void Post([FromBody]EpicMaster backlog)
        {
            _service.Add(backlog);

        }

        [HttpPut("{id}")]
        public void put(int id, [FromBody]EpicMaster value)
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
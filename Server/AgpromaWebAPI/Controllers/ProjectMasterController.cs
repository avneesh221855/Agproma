using Microsoft.AspNetCore.Mvc;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;
using AgpromaWebAPI.Viewmodel;
using System;
using System.Collections.Generic;

namespace AgpromaWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/ProjectMaster")]
    public class ProjectMasterController : Controller
    {// Service instances
        IProjectMasterService _service;
        public ProjectMasterController(IProjectMasterService service)
        {
            _service = service;
        }

        // GET: api/ProjectMaster
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProjectMaster/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                List<ProjectDetailView> promaster = _service.GetProjectById(id);
                if (promaster.Count == 0)
                {
                    return Ok("There are no Porjects");
                }
                return Ok(promaster);
            }

            catch (TimeoutException e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(102);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(500);
            }
        }


        // GET: api/ProjectMaster/5
        [HttpGet("GetProjectData/{id}")]
        public IActionResult GetProjectData(int id)
        {
            try
            {
                ProjectMaster promaster = _service.GetProjectData(id);
                if (promaster == null)
                {
                    return Ok("There are no Porjects");
                }
                return Ok(promaster);
            }

            catch (TimeoutException e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(102);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(500);
            }
        }

        // POST: api/ProjectMaster
        [HttpPost]
        public IActionResult Post([FromBody]ProjectMaster value)
        {
            try
            {
                _service.AddProject(value);
                _service.AddProjectmembersL(value);
                return Ok();
            }

            catch (TimeoutException e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(102);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(500);
            }
            
        }
        
        // PUT: api/ProjectMaster/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProjectMaster value)
        {
            try
            {
                _service.UpdateProject(id, value);
                return Ok();
            }

            catch (TimeoutException e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(102);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(500);
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.DeleteProject(id);
                return Ok();
            }

            catch (TimeoutException e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(102);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(500);
            }
        }
    }
}

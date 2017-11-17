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

    public class ChecklistController : Controller
    {
        private ICheckListService _context; // creating service instance
        public ChecklistController(ICheckListService context)
        {

            _context = context;
        }
        // GET: api/values
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            try
            {
                List<ChecklistBacklog> f = _context.Get(); //getting checklist
                return Ok(f);//return ok Response
            }
            catch
            {
                return BadRequest();// returns a response code of 400 if Exception
            }
        }

        // GET api/values/5
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult Get(int id)
       {
            try
            {
                if (id == null)
                {
                    return NoContent();
                }
                else
                {

                    List<ChecklistBacklog> checklist = _context.Get(id);
                    if (checklist.Count != 0)
                    {
                        return Ok(checklist); // returns a OK response if checklist returned are 0
                    }
                    else if(checklist.Count == 0)
                    {
                        return Ok("no data");
                    }
                    else
                    {
                        return NotFound(); // returns a response code 404 to the client
                    }
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetTaskDetail/{id}")]
        public IActionResult GetTaskDetail(int id) //Method for getting checklist by Id
        {
            try
            {

                TaskBacklog taskbl = _context.GetTaskDetail(id);
                return Ok(taskbl);// returns a response code of 200
            }
            catch
            {
                return BadRequest();// returns a response code of 400 if Exception
            }

        }

        // POST api/values
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult Post([FromBody]ChecklistBacklog value) //adding checklist
        {
            try
            {
                _context.Add_Checklist(value); //Call to service to checklist object
                return StatusCode(200, Ok());   // returns a response code of 200
            }
            catch (System.Exception)
            {
                return StatusCode(500);//returns 500 status when Exception
            }

        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult Delete(int id) //delete checklist
        {
            try
            {
                _context.Delete(id);  //Call to delete method with defined id
                return NoContent();  // returns a response code of 200
            }
            catch
            {
                return BadRequest();    //Returns 400 when Exception
            }
        }
        [HttpPut]
       [Route("api/[controller]/updateDailyStatus/{id}")]
        public IActionResult Put([FromBody]ChecklistBacklog checklist) //delete checklist
        {
            try
            {
                _context.Update_DailyStatus(checklist);  //Call to delete method with defined id
                return Ok();  // returns a response code of 200
            }
            catch
            {
                return BadRequest();    //Returns 400 when Exception
            }
        }


    }
}


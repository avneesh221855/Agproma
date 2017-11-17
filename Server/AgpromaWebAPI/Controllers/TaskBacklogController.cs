using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgpromaWebAPI.Service;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Viewmodel;
using Microsoft.AspNetCore.Authorization;

namespace AgpromaWebAPI.Controllers
{
    
    [Produces("application/json")]
    [Route("api/TaskBacklog")]
    public class TaskBacklogController : Controller
    {
        private ITaskBacklogService task;

        public TaskBacklogController(ITaskBacklogService tservice)
        {
            task = tservice;
        }

        //this method will return all the task in that same sprint
        [HttpGet("GetAllTaskDetail/{id}")]
        public List<TaskBacklogView> GetAllTaskDetail(int id)
        {
            var resp= task.GetAllTask(id);
            return resp;
        }

        [HttpGet("GetByTeamId/{id}")]
        public List<TeamMaster> GetByTeamId(int id)
        {
            return task.GetByTeamId(id);
        }

        [HttpGet("GetTask/{id}")]
        public List<TaskBacklog> GetTask(int id)
        {
            return null;
        }

        [HttpGet("GetTeamMember/{id}")]
        public List<AvailTeamMember> GetTeamMember(int id)
        {
            return task.getTeamMember(id);
        }

        [HttpGet("GetMemberName/{id}")]
        public string GetMemberName(int id)
        {
            
            return null;
        }

        // PUT: api/TaskBacklog/5
        [HttpPut("{id}")]
        public void Put(int id,[FromBody]AvailableMember member)
        {
            task.UpdateTask(member.MemberId, id);
        }
        
    }
}

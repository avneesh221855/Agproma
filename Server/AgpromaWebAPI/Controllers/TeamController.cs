using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgpromaWebAPI.Service;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Viewmodel;

namespace AgpromaWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        public ITeamService _service;
        public TeamController(ITeamService service)
        {
            _service = service;
        }

        // POST: api/team
        [HttpPost]
        public void Post([FromBody]TeamMaster team)
        {
            _service.AddTeam(team);
        }

        // PUT: api/team/5
        [HttpPost("UpdateteamMember")]
        public void UpdateteamMember([FromBody]TeamMember member)
        {
            _service.AddMembers(member);
        }

        // DELETE: api/team/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.DeleteMember(id);

        }
        // GET: api/team/1
        [HttpGet("{id}")]
        public List<TeamMaster> GetTeams(int id)
            {
            return _service.GetTeam(id);
        }
        // GET: api/team/1
        [HttpGet("GetAvailableMember/{id}")]
        public List<AvailTeamMember> GetAvailableMember(int id)
        {
            return _service.GetAvailableMember(id);
        }
        //GET: api/team
        [HttpGet("GetTeamMember/{id}")]
        public List<AvailTeamMember> getTeamMember(int id)
        {
            return null;
        }
    }
}
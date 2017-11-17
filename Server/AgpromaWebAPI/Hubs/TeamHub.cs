using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;
using AgpromaWebAPI.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Hubs
{
    public class TeamHub:Hub
    {
        public ITeamService _service;
        public ILogger<TeamHub> logger;
        public TeamHub(ITeamService service,ILogger<TeamHub> logger)
        {
            _service = service;
            this.logger = logger;
        }

        //call method to add memberinfo into db with connectionid and memberid
        public void SetConnectionId(int Memberid)
        {
            try
            {
                _service.UpdateConnectionId(Context.ConnectionId, Memberid);
            }
            catch (Exception n)
            {

                logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", n.TargetSite, n.Message, n.Source, n.ToString());
                throw;
            }

        }

        //this method will add team to a project
        public Task AddTeam(TeamMaster team)
        {
            try
            {
                _service.AddTeam(team);
                return Clients.Client(Context.ConnectionId).InvokeAsync("whenAdded", "success");
            }
            catch (Exception n)
            {

               logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", n.TargetSite, n.Message, n.Source, n.ToString());
               throw;
            }
        }

        //this method will add members to a team 
        public Task UpdateteamMember(TeamMember member,int projectId)
        {
            try
            {
                _service.AddMembers(member);
                return Clients.Client(Context.ConnectionId).InvokeAsync("whenUpdated", "success");
            }
            catch (Exception n)
            {
                logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", n.TargetSite, n.Message, n.Source, n.ToString());
                throw;
            }
           
        }

        //this method will delete member from a team
        public Task Delete(int id,int projectId)
        {
            try
            {
                _service.DeleteMember(id);
                return Clients.Client(Context.ConnectionId).InvokeAsync("whenDeleted", "success");
            }
            catch (Exception n)
            {
                logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", n.TargetSite, n.Message, n.Source, n.ToString());
                throw;
            }
           
        }

        //this method will return teams of a project
        public Task GetTeams(int id)
        {
            id = 12334;
            try
            {
                List<TeamMaster> teams = _service.GetTeam(id);
                return Clients.Client(Context.ConnectionId).InvokeAsync("getTeams", teams);
            }
            catch (Exception n)
            {
                logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", n.TargetSite, n.Message, n.Source, n.ToString());
                throw;
            }
            
        }

        //this method will return available members in a project
        public Task GetAvailableMember(int projectId)
        {
            try
            {
                List<AvailTeamMember> alist = _service.GetAvailableMember(projectId);
                return Clients.Client(Context.ConnectionId).InvokeAsync("getAvailableMember", alist);
            }
            catch (Exception n)
            {
                logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", n.TargetSite, n.Message, n.Source, n.ToString());
                throw;
            }
 
        }
    }
}


using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;
using System;
using System.Threading.Tasks;
namespace AgpromaWebAPI.Hubs
{
    public class ReleasePlansHub : Hub
    {
        private IReleasePlansService _service;
        ILogger<ReleasePlansHub> _logger;
        //constructor
        public ReleasePlansHub(IReleasePlansService service, ILogger<ReleasePlansHub> logger)
        {
            _service = service;
            _logger = logger;
        }

        public void SetConnectionId(int Memberid)
        {
            //call method to add memberinfo into db with connectionid and memberid
            _service.UpdateConnectionId(Context.ConnectionId, Memberid);
        }
        //method for creating a group to particular project id
        public void CreateGroup(int projectid)
        {
            try
            {
                var users = _service.CreateGroup(projectid);
                foreach (var user in users)
                {
                    Groups.AddAsync(user.ConnectionId, "releaseGroup");
                }
            }
            catch(Exception e)
            {
                _logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", e.TargetSite, e.Message, e.Source, e.ToString());
                throw;
            }
        }
        //method for getting all release plans to a aprticular project id
        public Task GetReleasePlans(int projectId)
        {
            try
            {
                CreateGroup(projectId);
                var data = _service.GetAllReleasePlan(projectId);
                return Clients.Client(Context.ConnectionId).InvokeAsync("getReleasePlans", data);
            }
            catch(Exception e)
            {
                _logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", e.TargetSite, e.Message, e.Source, e.ToString());
                throw;
            }
        }
        //method for adding a new release
        public Task AddRelease(ReleasePlan release)
        {
            try
            {
                CreateGroup(release.ProjectId);
                _service.AddRelease(release);
                return Clients.Group("releaseGroup").InvokeAsync("whenAdded", release);
            }
            catch(Exception e)
            {
                _logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", e.TargetSite, e.Message, e.Source, e.ToString());
                throw;
            }
        }
        //method for getting all sprints to a particular project id
        public Task GetAllSprints(int projectId)
        {
            try
            {
                CreateGroup(projectId);
                var data = _service.GetAllSprints(projectId);
                return Clients.Client(Context.ConnectionId).InvokeAsync("getsprints", data);
            }
            catch(Exception e)
            {
                _logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", e.TargetSite, e.Message, e.Source, e.ToString());
                throw;
            }
        }
        //Method for updating a release to a particular a sprint 
        public void UpdateReleaseInSprint(Sprint Sprints,int increment)
        {
            try
            {
                CreateGroup(Sprints.ProjectId);
                _service.UpdateReleaseInSprint(Sprints, increment);
                GetAllSprints(Sprints.ProjectId);
                GetReleasePlans(Sprints.ProjectId);
            }
            catch(Exception e)
            {
                _logger.LogError("Method {0}, Description: {1}, Source: {2},Exception: {3}", e.TargetSite, e.Message, e.Source, e.ToString());
                throw;
            }
        }
    }
}
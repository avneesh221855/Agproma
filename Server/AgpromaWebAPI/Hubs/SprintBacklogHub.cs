using Microsoft.AspNetCore.SignalR;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgpromaWebAPI.Viewmodel;

namespace AgpromaWebAPI.Hubs
{
    public class SprintBacklogHub : Hub
    {
        private ISprintService _service;
        private IStoryService _backlogService;
        private AgpromaDbContext _context;

        public SprintBacklogHub(ISprintService service,IStoryService backlogService,AgpromaDbContext context)
        {
            _service = service;
            _backlogService = backlogService;
            _context = context;
        }

        //call method to add memberinfo into db with connectionid and memberid
        public void SetConnectionId(int memberId)
        {
            _service.UpdateConnectionId(Context.ConnectionId, memberId);
        }

        //create a group and member join it each time they visited this Hub
        public void CreateGroup(int projectid)
        {
            var users = _service.CreateGroup(projectid);
            foreach (var user in users)
            {
                Groups.AddAsync(user.ConnectionId, "SprintGroup");
            }

        }

        //get all the sprints specific to projectId
        public void GetSprints(int projectid)
        {
            List<Sprint> data = _service.GetAll(projectid);
            Clients.Client(Context.ConnectionId).InvokeAsync("getSprints",data);
        }

        //add the sprint
        public Task AddSprint(Sprint sprint)
        {
            CreateGroup(sprint.ProjectId);
            _service.Add(sprint);
            return Clients.Group("SprintGroup").InvokeAsync("postSprints", sprint);
        }

        //get project details
        public Task GetProjectDetails(int projectId)
        {
            var project = _context.Projects.FirstOrDefault(m => m.ProjectId == projectId);
            return Clients.Client(Context.ConnectionId).InvokeAsync("projectDetails", project);
        }

        //update the sprint(assign story to sprint).
        public Task UpdateStoryInSprint(UserStory story, int sprintid,int projectid)
        {
             CreateGroup(projectid);
            _service.Update(sprintid,story);
            return GetAllBacklogs(projectid);
            //var unassigned = _service.GetUnassignedStories(projectid);
            //return Clients.Group("SprintGroup").InvokeAsync("updateSprint", sprintid,story,unassigned);
        }

        //get all the backlogs specific to projectId
        public Task GetAllBacklogs(int projectId)
        {
            CreateGroup(projectId);
            List<AssignedStory> backlogs=_service.GetassignedStories(projectId);
            var unassigned = _service.GetUnassignedStories(projectId);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getBacklogs", backlogs,unassigned);
        }

    }
}

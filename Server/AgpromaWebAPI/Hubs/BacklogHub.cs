using Microsoft.AspNetCore.SignalR;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AgpromaWebAPI.Hubs
{
    public class BacklogHub : Hub
    {
        private IStoryService _service;
        public BacklogHub(IStoryService service)
        {
            _service = service;
        }

        //set and update connection id for the user
        public void setConnectionId(int memberId)
        {
            _service.setConnectionId(Context.ConnectionId,memberId);
        }

        //create a group and member join it each time they visited this Hub
        public void JoinGroup(int projectId)
        {
            var users=_service.JoinGroup(projectId);
            foreach(var user in users)
            {
                Groups.AddAsync(user.ConnectionId, "BacklogGroup");
            }
        }
        
        //get the backlogs specific to projectId
        public void GetBacklog(int projectid)
        {
            var data = _service.GetAll(projectid);
            Clients.Client(Context.ConnectionId).InvokeAsync("getbacklog", data);
        }

        //add the backlog
        public Task PostBacklog(UserStory data)
        {
            JoinGroup(data.ProjectId);
            _service.Add(data);
            return Clients.Group("BacklogGroup").InvokeAsync("postBacklog", data);
        }

        //update the backlog(user story) according to storyId   
        public Task UpdateBacklog(UserStory product)
        {
            JoinGroup(product.ProjectId);
            UserStory backlog= _service.Update(product);
            return Clients.Group("BacklogGroup").InvokeAsync("updateBacklog",backlog);
        }

        // delete a backlog(user story) particular to storyId
        public Task DeleteBacklog(int storyId,int projectId)
        {
            JoinGroup(projectId);
            _service.Delete(storyId);
            return Clients.Group("BacklogGroup").InvokeAsync("deleteBacklog", storyId);
        }
    }
}
using Microsoft.AspNetCore.SignalR;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;
using AgpromaWebAPI.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AgpromaWebAPI.Hubs
{
    public class TaskHub : Hub
    {
        private ITaskServices _service;
        public TaskHub(ITaskServices service)
        {
            _service = service;
        }
        //update connection id for user id
        public void SetConnectionId(int memberId)
        {
            _service.SetConnectionId(Context.ConnectionId, memberId);
        }
        //add members in group
        public void JoinGroup(int projectId)
        {
            var members = _service.JoinGroup(projectId);
            foreach (var member in members)
            {
                Groups.AddAsync(member.ConnectionId, "TaskGroup");
            }
        }
        //get all task backlogs for a sprint ID.
        public Task GetTaskBacklogs(int UserStoryId)
        {
            int projectId = _service.GetProjectId(UserStoryId);
            JoinGroup(projectId);
            List<TaskBacklog> tasks = _service.GetAll(UserStoryId);
            return Clients.Client(Context.ConnectionId).InvokeAsync("gettask", tasks);
        }
        //Add a task to the DB.
        public Task PostTask(TaskBacklog task, int UserStoryId)
        {
            int projectId = _service.GetProjectId(UserStoryId);
            JoinGroup(projectId);
            _service.Add(task);
            return Clients.Group("TaskGroup").InvokeAsync("whenAdded", projectId);
        }
        //updates the task for the task id.
        public Task UpdateTask(int UserStoryId, TaskBacklog task)
        {
            int projectId = _service.GetProjectId(UserStoryId);
            JoinGroup(projectId);
            _service.Update(task.TaskId, task);
            return Clients.Group("TaskGroup").InvokeAsync("whenUpdated", projectId);
        }
    }
}

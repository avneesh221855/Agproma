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
    public class TaskBacklogHub:Hub
    {
        private ITaskBacklogService task;

        public TaskBacklogHub(ITaskBacklogService tservice)
        {
            task = tservice;
        }

        public void SetConnectionId(int Memberid)
        {
            //call method to add memberinfo into db with connectionid and memberid
            task.UpdateConnectionId(Context.ConnectionId, Memberid);
        }

        //this method will return all the task in that same sprint
        public Task GetAllTaskDetail(int id)
        {
            List<TaskBacklogView> tasks = task.GetAllTask(id);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getAllTaskDetail", tasks);
        }

        public Task GetTeamList(int id)
        {
            List<TeamMaster> teams = task.GetByTeamId(id);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getTeamList", teams);
        }

        public Task GetName(int sprintId)
        {
            List<AvailableMember> alist= task.GetName(sprintId);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getName",alist);
        }

        public Task GetTeamMember(int teamId)
        {
            List<AvailTeamMember> availablelist = task.getTeamMember(teamId);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getTeamMember", availablelist);
        }

        public Task AssignTask(int id,AvailableMember member)
        {
            task.UpdateTask(member.MemberId, id);
            return Clients.Client(Context.ConnectionId).InvokeAsync("whenAssigned", "success");
        }

        public Task GetProjectId(int sprint)
        {
            int n = task.GetProjectId(sprint);
            return Clients.Client(Context.ConnectionId).InvokeAsync("getProjectId",n);
        }
    }
}

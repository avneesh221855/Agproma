using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using AgpromaWebAPI.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Service
{
    public interface ITaskBacklogService
    {
        List<TaskBacklogView> GetAllTask(int id);
        List<TeamMaster> GetByTeamId(int SprintId);
        List<AvailTeamMember> getTeamMember(int teamId);
        void UpdateTask(int memberID, int taskId);
        void UpdateConnectionId(string connectionid, int memberid);
        List<AvailableMember> GetName(int id);
        int GetProjectId(int sprint);
    }
    public class TaskBacklogService : ITaskBacklogService
    {
        ITaskBacklogReposiory _taskBacklog;
        public TaskBacklogService(ITaskBacklogReposiory taskBacklog)
        {
            _taskBacklog = taskBacklog;
        }

        public List<TeamMaster> GetByTeamId(int SprintId)
        {
            int proId = 0;
            List<Sprint> allsprint = _taskBacklog.AllSprint();
            List<TeamMaster> teamMaster = _taskBacklog.getTeamName();
            List<TeamMaster> teamlistbyproject = new List<TeamMaster>();
            foreach (Sprint spr in allsprint)
            {
                if(spr.SprintId == SprintId)
                {
                    proId = spr.ProjectId;
                    break;
                }
            }
            if (proId != 0)
            {
                foreach (TeamMaster item in teamMaster)
                {
                    if (item.ProjectId == proId)
                    {
                        teamlistbyproject.Add(item);
                    }
                }
                return teamlistbyproject;
            }
            return teamlistbyproject;
        }

        public List<AvailableMember> GetName(int id)
        {
            List<AvailableMember> availteam = new List<AvailableMember>();
            Sprint sprint = _taskBacklog.GetProjectId(id);
            List<Projectmembers> projectmem = _taskBacklog.AllMember(sprint.ProjectId);
            foreach (Projectmembers pro in projectmem)
            {
                    User master = _taskBacklog.Master(pro.MemberId);
                    AvailableMember avail = new AvailableMember();
                    avail.MemberId = master.Id;
                    avail.MemberName = master.FirstName + ' ' + master.LastName;
                    availteam.Add(avail);
            }
            return availteam;
        }

        public List<AvailTeamMember> getTeamMember(int teamId)
        {
            List<AvailTeamMember> availteam = new List<AvailTeamMember>();
            List<TeamMember> teammas = _taskBacklog.AllTeamMember();
            foreach (TeamMember tm in teammas)
            {
                if (tm.TeamId == teamId)
                {
                    User master = _taskBacklog.Master(tm.MemberId);
                    AvailTeamMember avail = new AvailTeamMember();
                    avail.MemberId = master.Id;
                    avail.MemberName = master.FirstName + ' ' + master.LastName;
                    avail.TeamId = tm.TeamId;
                    avail.Id = tm.Id;
                    availteam.Add(avail);
                }
            }
            return availteam;
        }

        public void UpdateTask(int memberID, int taskId)
        {
            _taskBacklog.Update(memberID, taskId);
        }

        //this method will return all the task in that same sprint
        public List<TaskBacklogView> GetAllTask(int id)
        {
            List<TaskBacklogView> taskblv = new List<TaskBacklogView>();
            List<TaskBacklog> taskbacklog = _taskBacklog.GetAllTaskDetail(id);
            foreach (TaskBacklog tb in taskbacklog)
            {
                TaskBacklogView tblv = new TaskBacklogView();
                tblv.TaskId = tb.TaskId;
                tblv.TaskName = tb.TaskName;
                tblv.PersonId = tb.PersonId;
                tblv.SprintId = tb.SprintId;
                tblv.StartDate = tb.StartDate;
                tblv.ActualEndDate = tb.ActualEndDate;
                tblv.EndDate = tb.EndDate;
                tblv.Status = tb.Status;
                taskblv.Add(tblv);
            }
            return taskblv;
        }

        public void UpdateConnectionId(string connectionid, int memberid)
        {
            _taskBacklog.UpdateConnectionId(connectionid, memberid);
        }

        public int GetProjectId(int sprint)
        {
           Sprint spr = _taskBacklog.GetProjectId(sprint);
            return spr.ProjectId;
        }
    }
}

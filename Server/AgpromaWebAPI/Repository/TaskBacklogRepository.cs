using Microsoft.EntityFrameworkCore;
using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Repository
{
    public interface ITaskBacklogReposiory
    {
        List<TaskBacklog> getByTaskId();
        List<TeamMaster> getTeamName();
        List<Sprint> AllSprint();
        List<TeamMember> AllTeamMember();
        Sprint GetProjectId(int Id);
        User Master(int id);
        void UpdateConnectionId(string connectionid, int memberid);
        List<TaskBacklog> GetAllTaskDetail(int id);
        void Update(int memberId, int TaskId);
        List<Projectmembers> AllMember(int projectId);
    }
    public class TaskBacklogRepository : ITaskBacklogReposiory
    {
        private AgpromaDbContext _context;
        public TaskBacklogRepository(AgpromaDbContext context)
        {
            _context = context;
        }

        public User Master(int id)
        {
            return _context.Pmaster.FirstOrDefault(p => p.Id == id);
        }

        public List<Sprint> AllSprint()
        {
            return _context.Sprints.ToList();
        }

        public List<TeamMember> AllTeamMember()
        {
            return _context.Teammemeber.ToList();
        }

        public List<TaskBacklog> getByTaskId()
        {
            return _context.Tasks.ToList();

        }
        public List<TeamMaster> getTeamName()
        {
            return _context.Teammaster.ToList();
        }

        public void Update(int memberId, int TaskId)
        {
            TaskBacklog task = _context.Tasks.FirstOrDefault(p => p.TaskId == TaskId);
            task.PersonId = memberId;
            task.Status = TaskBacklogStatus.Inprogress;
            _context.SaveChanges();
        }

        //this method will return all the task in that same sprint from database
        public List<TaskBacklog> GetAllTaskDetail(int SprintId)
        {
            return _context.Tasks.Include(f=>f.SprintBacklogs).Where(p => p.SprintId == SprintId).ToList();
        }

        public void UpdateConnectionId(string connectionid, int memberid)
        {
            SignalRMaster signalr = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberid);
            signalr.ConnectionId = connectionid;
            signalr.HubCode = HubCode.taskbl;
            _context.SaveChanges();
        }

        public List<Projectmembers> AllMember(int projectId)
        {
            return _context.Projectmembers.Where(p => p.ProjectId == projectId).ToList();
        }

        public Sprint GetProjectId(int Id)
        {
            return _context.Sprints.FirstOrDefault(p => p.SprintId == Id);
        }
    }
}
using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Repository
{

    public interface ITaskRepository
    {
        List<SignalRMaster> JoinGroup(int projectId);
        void SetConnectionId(string connectionId,int memberId);
        List<TaskBacklog> GetAll(int sprintId);
        void Add(TaskBacklog bklog);
        void Update(int id, TaskBacklog bklog);
        Sprint GetProjectId(int sprintId);
        void Update_RemainingTime(ChecklistBacklog checklist);
    }
    public class TaskRepository : ITaskRepository
    {

        private AgpromaDbContext _context;
        public TaskRepository(AgpromaDbContext context)
        {
            _context = context;

        }

        public void Add(TaskBacklog bklog)
        {
            _context.Tasks.Add(bklog);
            _context.SaveChanges();
        }

        public List<TaskBacklog> GetAll(int storyId)
        {
            return _context.Tasks.Where(m=>m.StoryId==storyId).ToList();
        }

        public void Update(int id, TaskBacklog bklog)
        {
            TaskBacklog data = _context.Tasks.FirstOrDefault(m => m.TaskId == id);

            data.Title = bklog.Title;

            //  data. = bklog.Comments;
            _context.SaveChanges();
        }

        //updates the connection fields for the member
        public void SetConnectionId(string connectionId,int memberId)
        {
            SignalRMaster signalMaster = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberId);
            signalMaster.ConnectionId = connectionId;
            signalMaster.HubCode = HubCode.task;
            _context.SaveChanges();
        }

        //get the online member list and returned it
        public List<SignalRMaster> JoinGroup(int projectId)
        {
            List<Projectmembers> Projectmembers = _context.Projectmembers.Where(m => m.ProjectId == projectId).ToList();
            var memberIds = Projectmembers.Select(m => m.MemberId);
            List<SignalRMaster> onlineMembers = _context.SignalRDb.Where(m => m.Online == true && m.HubCode == HubCode.task).ToList();
            return onlineMembers.Where(n => memberIds.Contains(n.MemberId)).ToList();
        }

        public Sprint GetProjectId(int sprintId)
        {
            return _context.Sprints.FirstOrDefault(p => p.SprintId == sprintId);
        }
        public void Update_RemainingTime(ChecklistBacklog checklist)
        {
            TaskBacklog task = _context.Tasks.FirstOrDefault(m => m.TaskId == checklist.TaskId);
            task.Remaining = task.Remaining - checklist.RemainingSize;
            _context.SaveChanges();
        }
    }

}
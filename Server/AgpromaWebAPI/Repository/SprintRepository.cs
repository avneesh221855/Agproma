using Microsoft.EntityFrameworkCore;
using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgpromaWebAPI.Viewmodel;

namespace AgpromaWebAPI.Repository
{
    public interface ISprintRepository
    {
        List<Sprint> GetAll(int projectId);
        Sprint Get(int sprintId);
        void Add(Sprint sprint);
        void Update(int sprintId, UserStory sprint);
        void Delete(int sprintId);
        void UpdateConnectionId(string connectionid, int memberid);
        List<SignalRMaster> CreateGroup(int projectid);
        List<UserStory> GetUnassignedStories(int ProjectId);
        List<AssignedStory> GetassignedStories(int ProjectId);
    }

    public class SprintRepository : ISprintRepository
    {
        public AgpromaDbContext _context;
        public SprintRepository(AgpromaDbContext context)
        {
            _context = context;
        }

        //add a new sprint.
        public void Add(Sprint sprint)
        {
            sprint.Status = SprintStatus.New;
            _context.Sprints.Add(sprint);
            _context.SaveChanges();
        }

        //delete particular sprint.
        public void Delete(int sprintId)
        {
            Sprint sprint = _context.Sprints.FirstOrDefault(m => m.SprintId == sprintId);
            _context.Sprints.Remove(sprint);
            _context.SaveChanges();
        }

        //get the sprint backlog.
        public Sprint Get(int sprintId)
        {
            return _context.Sprints.FirstOrDefault(m => m.SprintId == sprintId);
        }

        //get all sprint backlogs.
        public List<Sprint> GetAll(int projectId)
        {
            return _context.Sprints.Where(m => m.ProjectId == projectId).ToList();
        }

        //updates sprint details of the specific sprint.
        public void Update(int sprintId, UserStory story)
        {
            Sprint sprint = _context.Sprints.FirstOrDefault(p => p.SprintId == sprintId);
            Sprint_UserStory sprint_User = new Sprint_UserStory();
            sprint.Status = SprintStatus.Inprogress;
            sprint.ActualSize += story.PlannedSize;
            sprint.PlannedSize += story.PlannedSize;
            sprint_User.SprintId = sprintId;
            sprint_User.StoryId = story.StoryId;
            _context.Sprint_UserStory.Add(sprint_User);
            _context.SaveChanges();
        }

        //get only unassigned stories for a project.
        public List<UserStory> GetUnassignedStories(int ProjectId)
        {
            var arrsprintid = _context.Sprint_UserStory.Select(u => u.StoryId);
            return _context.Userstories.Where(u=>!arrsprintid.Contains(u.StoryId)).ToList();
        }

        public void UpdateConnectionId(string connectionid, int memberid)
        {
            SignalRMaster signalr = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberid);
            signalr.ConnectionId = connectionid;
            signalr.HubCode = HubCode.sprint;
            signalr.Online = true;
            _context.SaveChanges();

        }
        public List<SignalRMaster> CreateGroup(int projectid)
        {
            List<Projectmembers> members = _context.Projectmembers.Where(p => p.ProjectId == projectid).ToList();

            List<SignalRMaster> onlinemembers = _context.SignalRDb.Where(m => m.Online == true && m.HubCode == HubCode.sprint).ToList();
            var data = members.Select(m => m.MemberId).ToList();
            var users = onlinemembers.Where(om => data.Contains(om.MemberId)).ToList();
            return users;
        }

        //get only assigned stories for a project.
        public List<AssignedStory> GetassignedStories(int ProjectId)
        {
           var data= (from a in _context.Userstories
             join b in _context.Sprint_UserStory on a.StoryId equals b.StoryId
             select new AssignedStory { StoryId = a.StoryId, StoryName=a.StoryName, SprintId=b.SprintId }).ToList();

            return data;
        }
    }
}


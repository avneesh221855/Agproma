using Microsoft.EntityFrameworkCore;
using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Repository
{

    public interface IStoryRepository
    {
        void setConnectionId(string connecitonId, int memberId);
        List<SignalRMaster> JoinGroup(int projectId);
        List<UserStory> GetAll(int  id);
        void Add(UserStory bklog);
        UserStory Update(UserStory bklog);
        void Delete(int id);
    }
    public class StoryRepository : IStoryRepository
    {

        private AgpromaDbContext _context;
        public StoryRepository(AgpromaDbContext context)
        {
            _context = context;
        }
 
        //for adding new item into the product backlog table
        public void Add(UserStory bklog)
        {
            //Adding  the ProductBacklog object
            _context.Userstories.Add(bklog);
            //persisting the changes made to the database
            _context.SaveChanges();
        }
 
        //for deleting any item based on the storyid
        public void Delete(int id)
        {
            //selecting  all the user story from the ProductBacklog Table by storyId passed by the client
            UserStory backlogToBeRemoved = _context.Userstories.FirstOrDefault(m => m.StoryId == id);

            //Chnaging status to Deleted of the Story object
            backlogToBeRemoved.Status = StoryStatus.Deleted;

            //persisting the changes made to the database
            _context.SaveChanges();
        }

        //for getting all user story based on project id
        public List<UserStory> GetAll(int  id)
        {
            //stories having projectid equals to id   will be fetched 
            return _context.Userstories.Where(r => r.ProjectId == id && r.Status!=StoryStatus.Deleted).Include(f => f.ProjectMaster).ToList();

           
        }
 
        //for updating a particular user story based on story id.
        public UserStory Update(UserStory bklog)
        {
            UserStory data = _context.Userstories.FirstOrDefault(m => m.StoryId == bklog.StoryId);
 
            //data will be updated
            data.StoryName = bklog.StoryName;
            data.Priority = bklog.Priority;
            data.Comments = bklog.Comments;
            data.PlannedSize = bklog.PlannedSize;
            _context.SaveChanges();
            return data;
        }
 
        //update the connection for a member.
        public void setConnectionId(string connectionId,int memberId)
        {
            SignalRMaster signalMaster = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberId);
            signalMaster.ConnectionId = connectionId;
            signalMaster.Online = true;
            signalMaster.HubCode = HubCode.backlog;
            //persisting the changes made to the database
            _context.SaveChanges();
        }
 
        //online members and on same component will get join the group.
        public List<SignalRMaster> JoinGroup(int projectId)
        {
            List<Projectmembers> Projectmembers = _context.Projectmembers.Where(m => m.ProjectId == projectId).ToList();
            var memberIds = Projectmembers.Select(m => m.MemberId);
            List<SignalRMaster> onlineMembers = _context.SignalRDb.Where(m => m.Online == true && m.HubCode == HubCode.backlog).ToList();
            return onlineMembers.Where(n => memberIds.Contains(n.MemberId)).ToList();
        }
    }

}




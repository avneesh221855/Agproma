using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AgpromaWebAPI.Repository
{
    //Interface
    public interface IReleasePlansRepo
    {
        List<Sprint> GetAllSprints(int projectId);
        List<ReleasePlan> GetAllRelease(int projectId);
        void AddRelease(ReleasePlan ReleasePlan);
        void UpdateConnectionId(string connectionid, int memberid);
        List<SignalRMaster> CreateGroup(int projectid);
        void UpdateReleaseInSprint(int sprintId,int increment);
    }
    public class ReleasePlansRepo : IReleasePlansRepo
    {
        AgpromaDbContext _context;
        //Constructor
        public ReleasePlansRepo(AgpromaDbContext context)
        {
            _context = context;
        }
        //Method for adding a new release
        public void AddRelease(ReleasePlan ReleasePlan)
        {
            ReleasePlan.Status = ReleasePlanStatus.New;
            ReleasePlan.Days = (ReleasePlan.EndDate - ReleasePlan.StartDate).Days;
            _context.ReleasePlans.Add(ReleasePlan);
            _context.SaveChanges();
        }
        //Method for getting the list of all release to a particular project id
        public List<ReleasePlan> GetAllRelease(int projectId)
        {
            return _context.ReleasePlans.Where( m=> m.ProjectId == projectId).ToList();
        }
        //Method for getting the list of all sprints to a particular project id
        public List<Sprint> GetAllSprints(int projectId)
        {
            return _context.Sprints.Where( m=>m.ProjectId == projectId).ToList();
        }
        //Method for updating a connection id through a member id
        public void UpdateConnectionId(string connectionid, int memberid)
        {
            SignalRMaster signalr = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberid);
            signalr.ConnectionId = connectionid;
            signalr.HubCode = HubCode.ReleasePlan;
            _context.SaveChanges();
        }
        //Method for creating a group to a particular project id
        public List<SignalRMaster> CreateGroup(int projectid)
        {
            List<Projectmembers> members = _context.Projectmembers.Where(m => m.ProjectId == projectid).ToList();
            List<SignalRMaster> onlinemembers = _context.SignalRDb.Where(m => m.Online == true && m.HubCode == HubCode.sprint).ToList();
            var data = members.Select(m => m.MemberId).ToList();
            var users = onlinemembers.Where(om => data.Contains(om.MemberId)).ToList();
            return users;
        }
        //Method for updating a release to a particular sprint id
        public void UpdateReleaseInSprint(int sprintId, int increment)
        {
            Sprint sprbl = _context.Sprints.FirstOrDefault(p => p.SprintId == sprintId);
            ReleasePlan replmaster = _context.ReleasePlans.FirstOrDefault(p => p.Increment == increment);
             if(sprbl.Status == SprintStatus.Inprogress)
            {
                replmaster.Status = ReleasePlanStatus.Inprogress;
            }
            sprbl.Increment = increment;
            _context.SaveChanges();
        }
    }
}

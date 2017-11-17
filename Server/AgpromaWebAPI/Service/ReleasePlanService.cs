using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using System.Collections.Generic;
namespace AgpromaWebAPI.Service
{
    //Interface
    public interface IReleasePlansService
    {
        List<ReleasePlan> GetAllReleasePlan(int id);
        void AddRelease(ReleasePlan ReleasePlan);
        List<Sprint> GetAllSprints(int id);
        void UpdateConnectionId(string connectionid, int memberid);
        List<SignalRMaster> CreateGroup(int projectid);
        void UpdateReleaseInSprint(Sprint Sprints,int increment);
    }
    public class ReleasePlansService : IReleasePlansService
    {
        private IReleasePlansRepo _ReleasePlansRepo;
        //Constructor
        public ReleasePlansService(IReleasePlansRepo ReleasePlansRepo)
        {
            _ReleasePlansRepo = ReleasePlansRepo;
        }
        //Method for adding a new release
        public void AddRelease(ReleasePlan ReleasePlan)
        {
            _ReleasePlansRepo.AddRelease(ReleasePlan);
        }
        //Method for getting a list of all release to a particular project id
        public List<ReleasePlan> GetAllReleasePlan(int id)
        {
            List<ReleasePlan> release = new List<ReleasePlan>();
            List<ReleasePlan> allRelease = _ReleasePlansRepo.GetAllRelease(id);
            foreach (ReleasePlan rem in allRelease)
            {
                if (rem.ProjectId == id)
                {
                    release.Add(rem);
                }
            }
            return release;
        }
        //Method for getting the list of all sprints to a particular project id
        public List<Sprint> GetAllSprints(int id)
        {
            return _ReleasePlansRepo.GetAllSprints(id);
        }
        //Method for updating a connection id
        public void UpdateConnectionId(string connectionid, int memberid)
        {
            _ReleasePlansRepo.UpdateConnectionId(connectionid, memberid);
        }
        //Method for creating a group of particular project id
        public List<SignalRMaster> CreateGroup(int projectid)
        {
            return _ReleasePlansRepo.CreateGroup(projectid);
        }
        //Method for updating a sprint in a release
        public void UpdateReleaseInSprint(Sprint Sprints, int increment)
        {
            _ReleasePlansRepo.UpdateReleaseInSprint(Sprints.SprintId, increment);
        }
    }
}
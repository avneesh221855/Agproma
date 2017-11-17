using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using AgpromaWebAPI.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Service
{
    public interface ISprintService
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
    public class SprintService : ISprintService
    {
        public ISprintRepository _repository;
        public SprintService(ISprintRepository repository)
        {
            _repository = repository;
        }
        public void Add(Sprint sprint)
        {
            _repository.Add(sprint);
        }

        public void Delete(int sprintId)
        {
            _repository.Delete(sprintId);
        }

        public Sprint Get(int sprintId)
        {
            return _repository.Get(sprintId);
        }

        public List<Sprint> GetAll(int projectId)
        {
            return _repository.GetAll(projectId);
        }

        public void Update(int sprintId, UserStory sprint)
        {
            _repository.Update(sprintId, sprint);
        }
        public void UpdateConnectionId(string connectionid, int memberid)
        {
            _repository.UpdateConnectionId(connectionid, memberid);
        }
        public List<SignalRMaster> CreateGroup(int projectid)
        {
            return _repository.CreateGroup(projectid);
        }
        public List<UserStory> GetUnassignedStories(int ProjectId)
        {
            return _repository.GetUnassignedStories(ProjectId);
        }

        public List<AssignedStory> GetassignedStories(int ProjectId)
        {
            return _repository.GetassignedStories(ProjectId);
        }
    }
}

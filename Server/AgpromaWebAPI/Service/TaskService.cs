using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using AgpromaWebAPI.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Service
{
    public interface ITaskServices
    {
        List<SignalRMaster> JoinGroup(int projectId);
        void SetConnectionId(string connectionId,int memberId);
        List<TaskBacklogView> GetAll(int sprintId);
        void Add(TaskBacklog backlog);
        void Update(int id, TaskBacklog res);
        int GetProjectId(int sprintId);
        void Update_RemainingTime(ChecklistBacklog checklist);
    }

    public class TaskService : ITaskServices
    {
        private ITaskRepository _repository;
        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public void Add(TaskBacklog backlog)
        {
            _repository.Add(backlog);
        }

        public List<TaskBacklogView> GetAll(int sprintId)
        {
            List<TaskBacklogView> taskblv = new List<TaskBacklogView>();
            List<TaskBacklog> taskbacklog = _repository.GetAll(sprintId);
            foreach(TaskBacklog tb in taskbacklog)
            {
                TaskBacklogView tblv = new TaskBacklogView();
                tblv.TaskId = tb.TaskId;
                tblv.TaskName = tb.Title;
                tblv.PersonId = tb.UserId;
               
                tblv.StartDate = tb.StartDate;
                tblv.ActualEndDate = tb.ActualEndDate;
                tblv.EndDate = tb.EndDate;
                tblv.Status = tb.Status;
                taskblv.Add(tblv);
            }
            return taskblv;
        }

        public void Update(int id, TaskBacklog res)
        {
            _repository.Update(id, res);
        }

        public void SetConnectionId(string connectionId,int memberId)
        {
            _repository.SetConnectionId(connectionId,memberId);
        }

        public List<SignalRMaster> JoinGroup(int projectId)
        {
            return _repository.JoinGroup(projectId);
        }

        public int GetProjectId(int sprintId)
        {
            Sprint sp = _repository.GetProjectId(sprintId);
            return sp.ProjectId;
        }
        public void Update_RemainingTime(ChecklistBacklog checklist)
        {
            _repository.Update_RemainingTime(checklist);
        }
    }
}
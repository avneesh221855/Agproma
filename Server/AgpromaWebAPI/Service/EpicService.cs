using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Service
{

    public interface IEpicServices
    {
        List<EpicMaster> GetAll(int id);
        void Add(EpicMaster backlog);
        void Update(int id, EpicMaster res);
        void Delete(int id);
        void SetConnectId(int userId,string conId);
        List<string> getGroup(int projectId);
    }

    public class EpicService:IEpicServices
    {
        
        private IEpicRepository _repository;
        public EpicService(IEpicRepository repository)
        {
            _repository = repository;
        }

        //for adding new epic 
        public void Add(EpicMaster backlog)
        {
            _repository.Add(backlog);
        }

        //for deleting partcular epic based on epic id
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        //for getting all epics based on projectid. 
        public List<EpicMaster> GetAll(int id)
        {
            return _repository.GetAll(id).ToList();
        }

        //for getting the group of users to which we want to show the update
        public List<string> getGroup(int projectId)
        {
           List<string> signal = new List<string>();
           List<Projectmembers> promem = _repository.GetMemberIdList(projectId);
           foreach(Projectmembers pro in promem)
           {
             SignalRMaster entry = _repository.GetConnectIdByMemId(pro.MemberId);
             if (entry.HubCode == HubCode.epic && entry.Online == true)
             { signal.Add(entry.ConnectionId); }
           }
            return signal;
        }

        //update the connection for the user
        public void SetConnectId(int userId,string conId)
        {
            _repository.SetConnectId(userId, conId);        
        }

        //for update particular epic based on project id 
        public void Update(int id, EpicMaster res)
        {
            _repository.Update(id, res);
        }
    }
}
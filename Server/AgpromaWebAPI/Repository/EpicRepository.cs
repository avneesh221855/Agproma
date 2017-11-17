using Microsoft.EntityFrameworkCore;
using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Repository
{

    public interface IEpicRepository
    {
        List<EpicMaster> GetAll(int id);
        void Add(EpicMaster bklog);
        void Update(int id, EpicMaster bklog);
        void Delete(int id);
        void SetConnectId(int userId,string conId);
        List<SignalRMaster> GetAllConnect();
        List<Projectmembers> GetMemberIdList(int id);
        SignalRMaster GetConnectIdByMemId(int memberId);
    }
    public class EpicRepository:IEpicRepository
    { 
        private AgpromaDbContext _context;
        public EpicRepository(AgpromaDbContext context)
        {
            _context = context;
         }
 
        //for adding new item into the product EpicDb table
        public void Add(EpicMaster bklog)
        {
            _context.EpicDb.Add(bklog);
            _context.SaveChanges();
        }
 
        //for deleting particular epic  based on epic id
        public void Delete(int id)
        {
            //selecting the Epics from the EpicDb Table by EpicId  passed by the client
            EpicMaster backlogToBeRemoved = _context.EpicDb.FirstOrDefault(m => m.EpicId == id);
            //Removing the Epic object
            _context.EpicDb.Remove(backlogToBeRemoved);
            //persisting the changes made to the database
           _context.SaveChanges();
        }
 
        //for getting all epics based on project id
        public List<EpicMaster> GetAll(int id)
        {
            return _context.EpicDb.Include(f => f.ProjectMaster).Where(r => r.ProjectId == id).ToList();
        }
        public List<SignalRMaster> GetAllConnect()
        {
            return _context.SignalRDb.ToList();
        }
 
        //for getting all members list specific to project id
        public List<Projectmembers> GetMemberIdList(int id)
        {
            List<Projectmembers> memidlist = new List<Projectmembers>();
            memidlist = _context.Projectmembers.Where(p => p.ProjectId == id).ToList();
            return memidlist;

        }

        //for updating connection for user.
        public void SetConnectId(int userId,string conId)
        {
            SignalRMaster sg = _context.SignalRDb.FirstOrDefault(p => p.MemberId == userId);
            sg.ConnectionId = conId;
            sg.HubCode = HubCode.epic;
            _context.SaveChanges();
        }

        //for getting user connection
        public SignalRMaster GetConnectIdByMemId(int memberId)
        {
            return _context.SignalRDb.FirstOrDefault(p => p.MemberId == memberId);
        }

        //for updating a particular epic based on epic id.
        public void Update(int id, EpicMaster bklog)
        {
            EpicMaster data = _context.EpicDb.FirstOrDefault(m => m.EpicId == id);
            data.Description = bklog.Description;
            //persisting the changes made to the database
            _context.SaveChanges();
        }
    }

}




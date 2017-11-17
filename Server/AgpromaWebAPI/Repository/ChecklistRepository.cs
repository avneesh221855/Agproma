using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgpromaWebAPI.model;
using Microsoft.EntityFrameworkCore;

namespace AgpromaWebAPI.Repository
{
    public interface ICheckListRepository //interface
    {
        TaskBacklog GetTaskDetail(int Id);
        List<ChecklistBacklog> Get();
        List<ChecklistBacklog> Get(int id);
        void Add_Checklist(ChecklistBacklog addchecklist);
        void Delete(int id);
        void Update_DailyStatus(ChecklistBacklog checklist);
    }
    public class ChecklistRepository : ICheckListRepository 
    {
        private AgpromaDbContext _context;
        public ChecklistRepository(AgpromaDbContext con) 
        {
            _context = con;

        }
        public void Add_Checklist(ChecklistBacklog addchecklist) //adding checklist
        {
            
            _context.Checklists.Add(addchecklist);
            _context.SaveChanges();
        }

        public void Delete(int id) //delete checklist
        {
            ChecklistBacklog f = _context.Checklists.FirstOrDefault(p => p.ChecklistId == id);
            _context.Checklists.Remove(f);
            _context.SaveChanges();
        }

        public List<ChecklistBacklog> Get() //getting checklist
        {
            return _context.Checklists.Include(m=>m.TaskBacklog).ToList();
        }

        public List<ChecklistBacklog> Get(int id) //getting checklist according to task
        {
            List<ChecklistBacklog> check = _context.Checklists.Where(Id => Id.TaskId == id).ToList();
            return check;
        }

        public TaskBacklog GetTaskDetail(int Id)
        {
            return _context.Tasks.FirstOrDefault(p => p.TaskId == Id);
        }
        public void Update_DailyStatus(ChecklistBacklog checklist)
        {
            ChecklistBacklog updatechecklist = _context.Checklists.FirstOrDefault(m => m.ChecklistId == checklist.ChecklistId);
            updatechecklist.RemainingSize = checklist.RemainingSize;
            updatechecklist.CompletedSize=checklist.CompletedSize;
            updatechecklist.Status = checklist.Status;
            _context.SaveChanges();
        }

    }
}

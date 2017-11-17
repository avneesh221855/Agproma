using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Service
{
    //interface of project member class
    public interface IProjectmembersRepository
    {
        List<Projectmembers> getMemberDetails(int id);
        void Add_MemberDetails(Projectmembers member);
    }
    public class ProjectmembersRepository : IProjectmembersRepository
    {
        //object of dbcontext class
        public AgpromaDbContext _context;
        //constructor
        public ProjectmembersRepository(AgpromaDbContext context)
        {
            _context = context;
        }
        //this method gets the details of particular project member

        public List<Projectmembers> getMemberDetails(int memberid)
        {
            return _context.Projectmembers.Where(p =>p.MemberId == memberid).ToList();
        }
        //this method adds the details of particular member corresponding to projectid
        public void Add_MemberDetails(Projectmembers member)
        {
            _context.Projectmembers.Add(member);
            _context.SaveChanges();
        }
    }
}

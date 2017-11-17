using Microsoft.EntityFrameworkCore;
using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Repository
{
    public interface IInviteRepository
    {
        List<Projectmembers> GetMemberName();
        User AllData(int id);
    }
    public class InviteRepository : IInviteRepository
    {
        public AgpromaDbContext _context;
        public InviteRepository(AgpromaDbContext context)
        {
            _context = context;
        }

        public User AllData(int id)
        {
            return _context.Users.FirstOrDefault(p => p.Id == id);
        }

        public List<Projectmembers> GetMemberName()
        {
            return _context.Projectmembers.ToList();
        }
    }
}
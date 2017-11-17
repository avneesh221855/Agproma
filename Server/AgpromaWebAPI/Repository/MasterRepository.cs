using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Repository
{
    public interface IMasterRepository
    {
        User getDetailsOfUser(int id);
        void updateDetails(int id, User details);
        List<User> getAll();
    }
    public class MasterRepository : IMasterRepository
    {
        private AgpromaDbContext _context;
        public MasterRepository(AgpromaDbContext context)
        {
            _context = context;
        }

        public List<User> getAll()
        {
            return _context.Users.ToList();
        }

        public User getDetailsOfUser(int userId)
        {
            User master = _context.Users.FirstOrDefault(p => p.Id == userId);
            return master;
        }
        public void updateDetails(int id,User details)
        {
            User m = _context.Users.FirstOrDefault(p => p.Id == id);
            m.Password = details.Password;

            _context.SaveChanges();
        }
    }
}



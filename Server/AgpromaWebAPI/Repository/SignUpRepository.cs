using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgProMa.Repository
{

    public interface ISignUpRepository
    {
        void UpdatePassword(int id, User master);
        User Get(string emailid);
        void Add_User(User adduser);
        void Update(string emailid, User user);
        List<User> GetAllDetails();

        User GetById(int id);

        bool SetLoggedIn(int userId, bool status);

    }
    public class SignUpRepository : ISignUpRepository
    {
        //object of dbcontext class
        private AgpromaDbContext _context;
        //constructor
        public SignUpRepository(AgpromaDbContext con)
        {
            _context = con;
        }
        //this method shows the list of all registered user details 
        public List<User> GetAllDetails()
        {
            return _context.Users.ToList();
        }

        //this method adds a particular user
        public void Add_User(User adduser)
        {
            _context.Users.Add(adduser);
            _context.SaveChanges();
            User m = Get(adduser.Email);
            SignalRMaster master=new SignalRMaster();

            master.MemberId = m.Id;
            master.Online = false;
            master.HubCode = HubCode.projectscreen;
           
            _context.SignalRDb.Add(master);
            _context.SaveChanges();
        }
        //this method get the details of a particular user    
        public User Get(string emailid)
        {
      
                User s = _context.Users.FirstOrDefault(p => p.Email == emailid);
                return s;
            
        }
        //this method updates the details of particular user
        public void Update(string emailid, User s)
        {
            User sign = _context.Users.FirstOrDefault(p => p.Email == emailid);
            sign.Password = s.Password;
            sign.Department = s.Department;
            sign.Organization = s.Organization;
            sign.FirstName = s.FirstName;
            sign.LastName = s.LastName;
            _context.SaveChanges();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(m => m.Id == id);
        }

        public void UpdatePassword(int id, User master)
        {
            var user = _context.Users.FirstOrDefault(m => m.Id == id);
            user.Password = master.Password;
            _context.SaveChanges();
            //this is used to set the status
        }
           public  bool SetLoggedIn(int userId, bool status)
            {
                SignalRMaster signaldata = _context.SignalRDb.FirstOrDefault(p => p.MemberId == userId);
                signaldata.Online = status;
                signaldata.HubCode = HubCode.projectscreen;
                _context.SaveChanges();
                return true;
            }
        }
    }


using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Service
{
    public interface IMasterService
    {
        User getUserDetailsService(int id);
        void updateDetails(int id, User details);

    }
    public class MasterService : IMasterService
    {
        public IMasterRepository _repo;
        public MasterService(IMasterRepository repo)
        {
            _repo = repo;
        }

        public User getUserDetailsService(int id)
        {
            User master =  _repo.getDetailsOfUser(id);
            return master;
        }
      public  void updateDetails(int id, User details)
        {
            _repo.updateDetails(id, details);
        }

    }
}

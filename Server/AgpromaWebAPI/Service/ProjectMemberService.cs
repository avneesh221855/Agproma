using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Service
{
    public interface IProjectmemberservice
    {
        List<Projectmembers> getMemberDetails(int id);
        int Add_MemberDetails(Projectmembers member);
       
    }
    public class Projectmemberservice : IProjectmemberservice
    {
        private IProjectmembersRepository _repo;
        public Projectmemberservice(IProjectmembersRepository repo)
        {
            _repo = repo;
        }

        public List<Projectmembers> getMemberDetails(int memberid)
        {
            
            return _repo.getMemberDetails(memberid).ToList();
        }

        public int Add_MemberDetails(Projectmembers details)
        {
            List<Projectmembers> projectmem = getMemberDetails(details.MemberId);
            foreach (var member in projectmem)
            {
                if ( member.MemberId == details.MemberId && member.ProjectId == details.ProjectId)
                {
                    return 0;
                }                    
            }
            _repo.Add_MemberDetails(details);
            return 1;



        }

    }
}

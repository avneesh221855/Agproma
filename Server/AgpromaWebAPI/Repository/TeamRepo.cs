using AgpromaWebAPI.model;
using System.Collections.Generic;
using System.Linq;

namespace AgpromaWebAPI.Repository
{
    public interface ITeamRepo
    {
        List<TeamMaster> GetTeam();
        User Master(int Id);
        List<Projectmembers> GetProjectmembers(int projectId);
        void UpdateConnectionId(string connectionid, int memberid);
        List<TeamMember> GetTeamMember(int teamId);
        void AddTeam(TeamMaster team);
        void AddMembers(TeamMember member);
        void DeleteMember(int id);
    }
    public class TeamRepo : ITeamRepo
    {
        private AgpromaDbContext _AgpromaDbContext;
        public TeamRepo(AgpromaDbContext _AgpromaDbContext)
        {
            this._AgpromaDbContext = _AgpromaDbContext;
        }
        //this method will add members to a team
        public void AddMembers(TeamMember member)
        {
            _AgpromaDbContext.Teammemeber.Add(member);
            _AgpromaDbContext.SaveChanges();
        }

        //this method will add team to a project
        public void AddTeam(TeamMaster team)
        {
            _AgpromaDbContext.Teammaster.Add(team);
            _AgpromaDbContext.SaveChanges();
        }

        //this method will delete member from a team
        public void DeleteMember(int id)
        {
            TeamMember member = _AgpromaDbContext.Teammemeber.FirstOrDefault(m => m.Id == id);
            _AgpromaDbContext.Teammemeber.Remove(member);
            _AgpromaDbContext.SaveChanges();
        }

        //this method will return project members
        public List<Projectmembers> GetProjectmembers(int id)
        {
            return _AgpromaDbContext.Projectmembers.Where(p => p.ProjectId == id).ToList();
        }

        //this method will return teams
        public List<TeamMaster> GetTeam()
        {
            return _AgpromaDbContext.Teammaster.ToList();
        }

        //this method will return team members of a particular team
        public List<TeamMember> GetTeamMember(int id)
        {
            return _AgpromaDbContext.Teammemeber.Where(p => p.TeamId == id).ToList();
        }

        public User Master(int Id)
        {
            return _AgpromaDbContext.Pmaster.FirstOrDefault(p => p.Id == Id);
        }

        //this method will update connection id of a member
        public void UpdateConnectionId(string connectionid, int memberid)
        {
            SignalRMaster signalr = _AgpromaDbContext.SignalRDb.FirstOrDefault(m => m.MemberId == memberid);
            signalr.ConnectionId = connectionid;
            signalr.HubCode = HubCode.team;
            _AgpromaDbContext.SaveChanges();
        }
    }
}
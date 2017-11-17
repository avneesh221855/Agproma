using Microsoft.EntityFrameworkCore;
using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgpromaWebAPI.Viewmodel;

namespace AgpromaWebAPI.Repository
{
    public interface IBurndownRepository
    {
        ProjectFullData GetProjectData(int projectId);
        List<TaskBacklog> GetTasks(int userId);
        List<Sprint> GetSprintDetails(int projectId);
        ProjectMaster GetProject(int projectId);
    }
    public class BurndownRepository : IBurndownRepository
    {
        public AgpromaDbContext _context;
        public BurndownRepository(AgpromaDbContext context)
        {
                _context=context;
        }

        //get project details for a project.
        public ProjectFullData GetProjectData(int projectId)
        {
            ProjectFullData profulldata = new ProjectFullData();
                profulldata.Release = (from r in _context.ReleasePlans
                               where r.ProjectId == projectId
                               join s in _context.Sprints
                               on r.Increment equals s.Increment  
                               select r).ToList();
                profulldata.Sprint = _context.Sprints.Where(p => p.ProjectId == projectId).ToList();

            profulldata.Stories = (from s in _context.Sprints
                                   where s.ProjectId == projectId
                                   join u in _context.Sprint_UserStory on s.SprintId equals u.SprintId
                                   join us in _context.Userstories.Include(p => p.Tasks).Include(p => p.Sprint_UserStory) on u.StoryId equals us.StoryId
                               select us).ToList();
            return profulldata;

        }
        //this is to bring all details of sprint
        public List<Sprint> GetSprintDetails(int projectId)
        {
            return _context.Sprints.Where(m=>m.ProjectId==projectId).ToList();
        }

        //get all tasks assigned to a user
        public List<TaskBacklog> GetTasks(int userId)
        {
            return _context.Tasks.Include(n=>n.Story.ProjectMaster).Where(m => m.UserId == userId && m.Status==TaskBacklogStatus.Completed).ToList();
        }

        //this method is to get the overall project velocity
        public ProjectMaster GetProject(int projectId)
        {
            return _context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using AgpromaWebAPI.model;
using Microsoft.EntityFrameworkCore;

namespace AgpromaWebAPI.Repository
{
    public interface IEfficiencyRepository //interface
    {
        List<TaskBacklog> GetEfficiencyForUser(int userId);
        List<TaskBacklog> GetEfficiencyForUserByProjectId(int userId);
    }

    public class EfficiencyRepository : IEfficiencyRepository
    {
        private AgpromaDbContext _context;
        public EfficiencyRepository(AgpromaDbContext context)
        {
            _context = context;
        }

        //get efficiency for user by project id 
        public List<TaskBacklog> GetEfficiencyForUserByProjectId(int userId)
        {
            //get project if for a user
            var task = _context.Tasks.Include(t => t.Story).FirstOrDefault(m => m.UserId == userId);
            int projectId = task.Story.ProjectId;

            //get story id's for a user within  a project.
            var stories = _context.Userstories.Where(m => m.ProjectId == projectId && m.UserId == userId).Select(s => s.StoryId).ToList();

            //get only task that are completed and assogned to a user.
            var tasks = _context.Tasks.Where(m => stories.Contains(m.StoryId) && m.UserId == userId && m.Status == TaskBacklogStatus.Completed).ToList();
            return tasks;
        }

        //get efficiency for a user by user id
        public List<TaskBacklog> GetEfficiencyForUser(int userId)
        {
            //get tasks for a user that are completed.
            return _context.Tasks.Where(m => m.UserId == userId && m.Status == TaskBacklogStatus.Completed).ToList();
        }
    }
}
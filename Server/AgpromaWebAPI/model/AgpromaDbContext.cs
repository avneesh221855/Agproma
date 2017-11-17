using Microsoft.EntityFrameworkCore;

namespace AgpromaWebAPI.model
{
    public class AgpromaDbContext:DbContext
    {//this model is database model which is used to make the table in my database
        public AgpromaDbContext(DbContextOptions options) : base(options)
        {

        }
            public DbSet<ChecklistBacklog> Checklists  { get; set; }
            public DbSet<User> Users  { get; set; }
            public DbSet<UserStory> Userstories { get; set; }
            public DbSet<ReleasePlan> ReleasePlans { get; set; }
            public DbSet<ProjectMaster> Projects { get; set; }
            public DbSet<Sprint> Sprints { get; set; }
            public DbSet<Sprint_UserStory> Sprint_UserStory { get; set; }
            public DbSet<TaskBacklog> Tasks { get; set; }
            public DbSet<TaskBurnDown> TaskBurnDowns { get; set; }
            public DbSet<TeamMaster> Teams { get; set; }
            public DbSet<TeamMember> Teammembers { get; set; }
            public DbSet<Projectmembers> Projectmembers { get; set; }
            public DbSet<SignalRMaster> SignalRDb { get; set; }
            public DbSet<EpicMaster> EpicDb { get; set; }
    }
}


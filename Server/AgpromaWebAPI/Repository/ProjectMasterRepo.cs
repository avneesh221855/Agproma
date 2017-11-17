using AgpromaWebAPI.model;
using AgpromaWebAPI.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Repository
{ //interface of project creation and manipulation repository
    public interface IProjectMasterRepo
    {
        List<Projectmembers> GetRoleOf();
        List<ProjectMaster> GetAllProjects();
        void AddNewProject(ProjectMaster promas);
        void DeleteProject(int Id);
        void UpdateProject(int Id,ProjectMaster promas);
        void AddProjectmembersL(Projectmembers promem);
        ProjectMaster GetProjectData(int projectId);
    }
    //class used for containing method of this repository
    public class ProjectMasterRepo : IProjectMasterRepo
    {   
        //constructor of this method
        AgpromaDbContext _context;
        public ProjectMasterRepo(AgpromaDbContext context)
        {
            _context = context;
        }
        //method to add new project
        public void AddNewProject(ProjectMaster promas)
        {
            _context.Projects.Add(promas);
            _context.SaveChanges();
        }
        //this is to delete the project
        public void DeleteProject(int Id)
        {
            ProjectMaster promas = _context.Projects.FirstOrDefault(p => p.ProjectId == Id);
            _context.Projects.Remove(promas);
            _context.SaveChanges();
        }

        //method to get all the project in our table
        public List<ProjectMaster> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        //this project is used to update the project
        public void UpdateProject(int Id, ProjectMaster promas)
        {
            ProjectMaster promaster = _context.Projects.FirstOrDefault(p => p.ProjectId == Id);
            promaster.ProjectDescription = promas.ProjectDescription;
            promaster.TechnologyUsed = promas.TechnologyUsed;
            promaster.Name = promas.Name;
            _context.SaveChanges();
        }

        //this method is used for Adding leader in project member table
        public void AddProjectmembersL(Projectmembers promem)
        {
            _context.Projectmembers.Add(promem);
            _context.SaveChanges();
        }
        //this is to get the role of particular user id
        public List<Projectmembers> GetRoleOf()
        {
            return _context.Projectmembers.ToList();
        }

        public ProjectMaster GetProjectData(int projectId)
        {
            return _context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
        }
    }
}

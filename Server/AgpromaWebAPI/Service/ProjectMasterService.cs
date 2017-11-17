using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using AgpromaWebAPI.Viewmodel;
using System;
using System.Collections.Generic;

namespace AgpromaWebAPI.Service
{   
    //interface id used for making method to be implemented
    public interface IProjectMasterService
    {
        void AddProject(ProjectMaster projectmas);
        List<ProjectDetailView> GetProjectById(int Id);
        void DeleteProject(int Id);
        void UpdateProject(int Id,ProjectMaster projectmas);
        void AddProjectmembersL(ProjectMaster promas);
        ProjectMaster GetProjectData(int projectId);
    }
    public class ProjectMasterService:IProjectMasterService
    {
        //constructor of this class
            private IProjectMasterRepo _repocontext;
            public ProjectMasterService(IProjectMasterRepo repocontext)
            {
            _repocontext = repocontext;
            }

        //method for addition of project
        public void AddProject(ProjectMaster projectmas)
        {
            int count = 0;
            List<ProjectMaster> prolist = _repocontext.GetAllProjects();
            foreach (ProjectMaster pro in prolist)
            {              
                if (pro.Name == projectmas.Name)
                {
                    count++;
                    throw new Exception("already exist");
                }
            }
            if (count == 0)
            { _repocontext.AddNewProject(projectmas); }
        }
        //this is to add the leader in project member table
        public void AddProjectmembersL(ProjectMaster promas)
        {
            Projectmembers promem = new Projectmembers();
            promem.ProjectId = promas.ProjectId;
            promem.MemberId = promas.CreatedBy;
            promem.ActAs = As.leader;
            _repocontext.AddProjectmembersL(promem);
        }

        //method is used to delete the project which are not needed
        public void DeleteProject(int Id)
        {
            _repocontext.DeleteProject(Id); 
        }

        //method to fetch the project related to the respective person who is the leader
        public List<ProjectDetailView> GetProjectById(int Id)
        {
            List<ProjectDetailView> projectdetail = new List<ProjectDetailView>();
            List<ProjectMaster> prolist = _repocontext.GetAllProjects();
            List<Projectmembers> prodetvie = _repocontext.GetRoleOf();
            foreach (Projectmembers promem in prodetvie)
            {
                if (promem.MemberId == Id)
                {
                    foreach(ProjectMaster pro in prolist)
                    {
                        if(pro.ProjectId == promem.ProjectId)
                        {
                            ProjectDetailView prodetail = new ProjectDetailView()
                            {
                                ProjectId = pro.ProjectId,
                                Name = pro.Name,
                                ProjectDescription = pro.ProjectDescription,
                                TechnologyUsed = pro.TechnologyUsed,
                                ActAs = promem.ActAs
                            };
                            projectdetail.Add(prodetail);
                        }
                    }
                }
            }
            return projectdetail;
        }

        public ProjectMaster GetProjectData(int projectId)
        {
            return _repocontext.GetProjectData(projectId);
        }

        //this is to update the project
        public void UpdateProject(int Id, ProjectMaster projectmas)
        {
            _repocontext.UpdateProject(Id, projectmas);
        }
    }
}

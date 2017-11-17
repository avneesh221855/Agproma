using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using AgpromaWebAPI.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Service
{
    public interface IBurndownService
    {
        ProjectFullData GetProjectData(int projectId);
        List<UserBurnDown> GetTasks(int userId);
        List<SprintChart> GetSprintDetails(int projectId);
        VelocityChart GetVelocityDetails(int projectId);
    }

    public class BurndownService : IBurndownService
    {
        public IBurndownRepository _repository;
        public BurndownService(IBurndownRepository repository)
        {
            _repository = repository;
        }

        //get all tasks assign to a user
        public List<UserBurnDown> GetTasks(int userId)
        {
            List<UserBurnDown> dropdown = new List<UserBurnDown>();
            //get all tasks for a user
            List<TaskBacklog> tasks= _repository.GetTasks(userId);
            tasks.ForEach(m => {
                UserBurnDown userbd = new UserBurnDown();
                userbd.TaskId = m.TaskId;
                userbd.TaskName = m.Title;
                userbd.ExpectedDate = m.EndDate.Subtract(m.StartDate).TotalHours;
                userbd.ActualDate = m.ActualEndDate.Subtract(m.StartDate).TotalHours;
                userbd.ProjectId =m.Story.ProjectId;
                userbd.ProjectName = m.Story.ProjectMaster.Name;
                userbd.StoryName=m.Story.StoryName;
                //Add each task details for a user
                dropdown.Add(userbd);
            });
            return dropdown;
        }

        //get all project details specific to a project.
        public ProjectFullData GetProjectData(int projectId)
        {
            var projectData= _repository.GetProjectData(projectId);
            return projectData;
        }

        public List<SprintChart> GetSprintDetails(int projectId)
        {
            int total = 140;
            int count = 0;
            List<SprintChart> sprintchart = new List<SprintChart>();
            List<Sprint> sprints = _repository.GetSprintDetails(projectId);
            foreach(Sprint spr in sprints)
            {
                SprintChart sprcht = new SprintChart();
                if (count == 0) { sprcht.RemainWork = total; }
                else {
                    total = total - spr.ActualSize;
                    sprcht.RemainWork = total;
                }
                sprcht.TotalSize = 140;               
                sprcht.PlannedWork = spr.PlannedSize;
                sprcht.RealizedWork = spr.ActualSize;
                sprcht.Sprint = spr.SprintId;
                sprintchart.Add(sprcht);
                count++;
            }
            return sprintchart;
        }

        public VelocityChart GetVelocityDetails(int projectId)
        {
            int sum = 0;
            VelocityChart velocitychart = new VelocityChart();
            velocitychart.OriginalEstimate = _repository.GetProject(projectId).Velocity;
            List<Sprint> sprints = _repository.GetSprintDetails(projectId);
            velocitychart.LastOneSprints = sprints.Last().ActualSize;
            foreach (Sprint srp in sprints)
            {
                sum = sum + srp.ActualSize;
            }
            velocitychart.RealizedTotalAverage = sum / sprints.Count;
            if (sprints.Count >= 8)
            {
                sum = 0;
                IEnumerable<Sprint> lasteightsprints = sprints.TakeLast(8);
                foreach (Sprint spr in lasteightsprints)
                {
                    sum = sum + spr.ActualSize;
                }
                velocitychart.AvgLastEight = sum / 8;
                lasteightsprints.OrderByDescending(p => (p.ActualSize - p.PlannedSize));
                velocitychart.AvgWorstThreeInLastEight = lasteightsprints.TakeLast(3).Sum(p => p.ActualSize);
            }
            else
            {
                velocitychart.AvgLastEight = sum / sprints.Count;
            }
            return velocitychart;
        }
    }
}

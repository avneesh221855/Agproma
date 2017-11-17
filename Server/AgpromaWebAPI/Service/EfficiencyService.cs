using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Service
{
    public interface IEfficiencyService
    {
        float GetEfficiencyForUser(int userId);
        float GetEfficiencyForUserByProjectId(int userId);
    }
    public class EfficiencyService : IEfficiencyService
    {
        public IEfficiencyRepository _repository;
        public EfficiencyService(IEfficiencyRepository repository)
        {
            _repository = repository;
        }

        //get efficiency for a user.
        public float GetEfficiencyForUser(int userId)
        {
            //get all tasks assigned to a user.
            List<TaskBacklog> tasks=_repository.GetEfficiencyForUser(userId);
            float expectedTime = 0, actualTime = 0;
            //get expected time and actual time for all the tasks assigned to single user only.
            foreach (var task in tasks)
            {
                expectedTime += task.PlannedSize;
                actualTime += task.ActualSize;
            }
            if(tasks.Count()== 0)
            {
                return 0;
            }
            return (expectedTime / actualTime) * 100;
        }

        //get efficiency for a user by project.
        public float GetEfficiencyForUserByProjectId(int userId)
        {
            //get all tasks assigned to a user.
            List<TaskBacklog> tasks = _repository.GetEfficiencyForUserByProjectId(userId);
            float expectedTime = 0, actualTime = 0;
            //get expected time and actual time for all the tasks assigned to single user only.
            foreach (var task in tasks)
            {
                expectedTime += task.PlannedSize;
                actualTime += task.ActualSize;
            }
            if (tasks.Count() == 0)
            {
                return 0;
            }
            return (expectedTime / actualTime) * 100;
        }
        
    }
}

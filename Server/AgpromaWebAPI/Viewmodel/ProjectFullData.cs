using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Viewmodel
{
    public class ProjectFullData
    {
        public List<ReleasePlan> Release { get; set; }
        public List<Sprint> Sprint { get; set; }
        public List<UserStory> Stories { get; set; }
    }
}

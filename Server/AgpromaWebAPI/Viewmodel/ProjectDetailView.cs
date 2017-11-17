using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Viewmodel
{
    public class ProjectDetailView
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string ProjectDescription { get; set; }

        public string TechnologyUsed { get; set; }

        public As ActAs { get; set; }
    }
}

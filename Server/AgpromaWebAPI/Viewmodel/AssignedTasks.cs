using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Viewmodel
{
    public class AssignedStory
    {
        public int SprintId { get; set; }
        public int StoryId { get; set; }
        public string StoryName { get; set; }
    }
}

using AgpromaWebAPI.model;

namespace AgpromaWebAPI.Viewmodel
{
    public class UserBurnDown
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public string ProjectName { get; set; }

        public string StoryName { get; set; }

        public int ProjectId { get; set; }

        public double ActualDate { get; set; }

        public double ExpectedDate { get; set; }
    }
}

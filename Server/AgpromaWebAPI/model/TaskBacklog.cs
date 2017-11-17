using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.model
{
    public enum TaskBacklogStatus
    {
        New,
        Inprogress,
        Completed,
        Slipped
    }
    public class TaskBacklog
    {
        [Key]
        public int TaskId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }    

        public int UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PlannedSize { get; set; }

        public int ActualSize { get; set; }

        public DateTime ActualEndDate { get; set; }

        public int Remaining { get; set; }

        [EnumDataType(typeof(TaskBacklogStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public TaskBacklogStatus Status { get; set; }

        public int StoryId { get; set; }
        [ForeignKey("StoryId")]
        public UserStory Story { get; set; }
    }
}

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
    public enum SprintStatus
    {
        New,
        Inprogress,
        Completed,
        Slipped
    }

    public class Sprint
    {
        [Key]
        public int SprintId { get; set; }

        public string SprintName { get; set; }

        public string SprintGoal { get; set; }

        public int TotalDays { get; set; }

        public int Increment { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PlannedSize { get; set; }

        public int ActualSize { get; set; }

        [EnumDataType(typeof(SprintStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public SprintStatus Status { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectMaster ProjectMaster { get; set; }

    }
}

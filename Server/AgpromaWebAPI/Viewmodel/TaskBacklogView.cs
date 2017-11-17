using AgpromaWebAPI.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Viewmodel
{
    public class TaskBacklogView
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int PersonId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ActualEndDate { get; set; }

        [EnumDataType(typeof(TaskBacklogStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public TaskBacklogStatus Status { get; set; }

        public int SprintId { get; set; }
    }
}

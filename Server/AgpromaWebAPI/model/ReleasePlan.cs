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
    public enum ReleasePlanStatus
    {
        New,
        Inprogress,
        Released
    }
    public class ReleasePlan
    {//it will store the data of release plan of the project
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReleasePlanId { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectMaster ProjectMaster { get; set; }

        public string ReleaseName { get; set; }

        public int Increment { get; set; }

        public string Goal { get; set; }

        public int Days { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        [EnumDataType(typeof(ReleasePlanStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReleasePlanStatus Status { get; set; }
    }
}

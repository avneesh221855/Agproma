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
    public enum HubCode
    {
        epic,
        backlog,
        sprint,
        taskbl,
        task,
        team,
        ReleasePlan,
        projectscreen
    }
    public class SignalRMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SignalId { get; set; }

        public int MemberId { get; set; }

        public string ConnectionId { get; set; }

        public bool Online { get; set; }

        [EnumDataType(typeof(HubCode))]
        [JsonConverter(typeof(StringEnumConverter))]
        public HubCode HubCode { get; set; }

    }
}

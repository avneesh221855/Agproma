using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.model
{
    public class ProjectMaster
    {//store the data of the new project added
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string ProjectDescription { get; set; }

        public string TechnologyUsed { get; set; }

        public int CreatedBy { get; set; }

        public string ApprovedBy { get; set; }

        public string ReviewedBy { get; set; }

        public string Milestone { get; set; }

        public int Velocity { get; set; }

        public int Rhythm { get; set; }

        public string ModeOfOperation { get; set; }
    }
}

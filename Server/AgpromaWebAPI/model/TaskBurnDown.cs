using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.model
{
    public class TaskBurnDown
    {
        [Key]
        public int TaskBurnDownId { get; set; }

        public int UserId { get; set; }

        public int PointCompleted { get; set; }

        public int PointRemaining { get; set; }

        public int CheckListId { get; set; }

        public DateTime Date { get; set; }

        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual TaskBacklog TaskBacklog { get; set; }
    }
}

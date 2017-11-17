using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.model
{
    public class Sprint_UserStory
    {
        [Key]
        public int Id { get; set; }

        public int SprintId { get; set; }
        [ForeignKey("SprintId")]
        public Sprint Sprint { get; set; }

        public int StoryId { get; set; }
        [ForeignKey("StoryId")]
        public UserStory Story { get; set; }
    }
}

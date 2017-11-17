using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Viewmodel
{
    public class AvailTeamMember
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public int MemberId { get; set; }

        public string MemberName { get; set; }
    }
}

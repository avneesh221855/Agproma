using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Viewmodel
{
    public class InviteExistingMember
    {
        public int ProjectId { get; set; }

        public string MemberName { get; set; }

        public string Email { get; set; }
    }
}

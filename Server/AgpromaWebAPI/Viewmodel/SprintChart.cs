using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Viewmodel
{//this view model is used to fill the data in sprint burn down chart
    public class SprintChart
    {
        public int Sprint { get; set; }

        public int RemainWork { get; set; }

        public int PlannedWork { get; set; }

        public int RealizedWork { get; set; }

        public int TotalSize { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Viewmodel
{
    public class VelocityChart
    {
        public int OriginalEstimate { get; set; }

        public int LastOneSprints { get; set; }

        public int RealizedTotalAverage { get; set; }

        public int AvgLastEight { get; set; }

        public int AvgWorstThreeInLastEight { get; set; }

    }
}

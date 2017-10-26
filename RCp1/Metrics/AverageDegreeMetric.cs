using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCp1.Data;

namespace RCp1.Metrics
{
    public class AverageDegreeMetric : INetworkMetric
    {
 
        public string GetDisplayName()
        {
            return "Average Degree";
        }

        public  INetworkMetric Copy()
        {
            return new AverageDegreeMetric();
        }

        public double Analyze(RandomNetwork pNetwork, bool pDirected)
        {
            return 0.0;
        }
    }
}

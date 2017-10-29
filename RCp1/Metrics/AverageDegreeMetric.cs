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

        public double Analyze(RandomNetwork pNetwork, bool pDirected)
        {
            double averageDegree = 0;
            
            double N = pNetwork.Nodes().Count;
            
            double E = pNetwork.Edges().Count;
            
            E *= 2.0d;
            
            averageDegree = E / N;
            
            return averageDegree;
        }
    }
}

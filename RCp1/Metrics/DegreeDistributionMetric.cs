using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCp1.Data;

namespace RCp1.Metrics
{
    public class DegreeDistributionMetric : INetworkMetric
    {

        public string GetDisplayName()
        {
            return "Degree Distribution";
        }


        public INetworkMetric Copy()
        {
            return new DegreeDistributionMetric();
        }
        
        public double Analyze(RandomNetwork network, bool directed)
        {
           
            int N = network.GetNumNodes();

            
            int[] degree = new int[N];


            foreach (var node in network.Nodes())
            {
                var edges = network.EdgesAdjacent(node, directed, directed, !directed);
                int nodeDegree = edges.Count;
                degree[nodeDegree]++;
            }

            return LeastSquares(degree)[0];
        }

        public Dictionary<int, int> Analyze(RandomNetwork network)
        {
            var distribuitionDegree = new Dictionary<int, int>();
            foreach (var vertice in network.Nodes())
            {
                int degree = network.MGraph.AdjacentDegree(vertice);

                if (distribuitionDegree.ContainsKey(degree))
                {
                    distribuitionDegree[degree] += 1;

                }
                else
                {
                    distribuitionDegree.Add(degree, 1);
                }
            }

            return distribuitionDegree;
        }


        public double[] LeastSquares(int [] dist)
        {

            //Vararibles to compute
            double SSxx = 0;
            double SSyy = 0;
            double SSxy = 0;


            //Compute the average log(x) value when for positive (>0) values
            double avgX = 0;
            double nonZero = 0;
            for (int i = 1; i < dist.Length; i++)
            {
                if (dist[i] > 0)
                {
                    avgX += Math.Log(i);
                    nonZero++;
                }
            }
            avgX /= nonZero;

            //compute the variance of log(x)
            for (int i = 1; i < dist.Length; i++)
            {
                if (dist[i] > 0)
                {
                    SSxx += Math.Pow(Math.Log(i) - avgX, 2);
                }
            }


            //Compute the average log(y) values
            double avgY = 0;
            for (int i = 1; i < dist.Length; i++)
            {
                if (dist[i] > 0)
                {
                    avgY += Math.Log(dist[i]);
                }
            }
            avgY /= nonZero;

            //compute the variance over the log(y) values
            for (int i = 1; i < dist.Length; i++)
            {
                if (dist[i] > 0)
                {
                    SSyy += Math.Pow(Math.Log(dist[i]) - avgY, 2);
                }
            }


            //Compute teh SSxy term
            for (int i = 1; i < dist.Length; i++)
            {
                if (dist[i] > 0)
                {
                    SSxy += (Math.Log(i) - avgX) * (Math.Log(dist[i]) - avgY);
                }
            }


            //Compute and return the results
            double [] results = new double[4];
            results[0] = SSxy / SSxx;
            results[1] = avgY - results[0] * avgX;
            results[2] = (SSxy * SSxy) / (SSxx * SSyy);
            results[3] = SSxy / nonZero;

            return results;

        }
    }
}

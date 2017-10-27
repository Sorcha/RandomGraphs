using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using RCp1.Data;

namespace RCp1.Metrics
{
    public class ClusteringCoefficientMetric : INetworkMetric
    {

        public string GetDisplayName()
        {
            return "Clustering Coefficient";
        }


        public INetworkMetric Copy()
        {
            return new ClusteringCoefficientMetric();
        }


        public double Analyze(RandomNetwork pNetwork, bool pDirected)
        {
            double averageClusteringCoefficient = 0;
            
            List<int> nodeIterator = pNetwork.Nodes();
            
            int N = nodeIterator.Count;
            
            List<int> []nodeInRep = new List<int>[N];
            List<int>[] nodeOutRep = new List<int>[N];

            int nodeCount = 0;

            foreach (var node in nodeIterator)
            {
                nodeOutRep[node] = new List<int>();
                if (pDirected)
                {
                   
                    nodeInRep[node] = new List<int>();
                }

                var edgeIterator = pNetwork.EdgesAdjacent(node, pDirected, false, !pDirected);

                foreach (var edge in edgeIterator)
                {
                    int neighborIndex = edge.Target;
                    
                    nodeOutRep[node].Add(neighborIndex);
                }

                if (pDirected)
                {
                    edgeIterator = pNetwork.EdgesAdjacent(node, false, true, false);
                    foreach (var edge in edgeIterator)
                    {
                        
                        int neighborIndex = edge.Target;
                        
                        nodeInRep[node].Add(neighborIndex);
                    }
                }
            }

          
            for (int i = 0; i < N; i++)
            {
                
                double edgeCount = 0;

                
                List<int> neighborhood =nodeOutRep[i].ToList();

                if (pDirected)
                {
                    foreach (var node in nodeInRep[i])
                    {

                      
                        if (!neighborhood.Contains(node))
                        {
                            neighborhood.Add(node);
                        }
                    }
                   
                }
                
                if (neighborhood.Count < 2)
                {
                    continue;
                }
                
                double size = neighborhood.Count * (neighborhood.Count - 1);

                while (neighborhood.Count > 0)
                {

                    int neighbor1 = neighborhood.ElementAt(0);
                    neighborhood.RemoveAt(0);

          
                    foreach (var neighbor2 in neighborhood)
                    {

                        if (nodeOutRep[neighbor1].Contains(neighbor2))
                        {

                            edgeCount++;
                        }

                        if ((pDirected) && (nodeOutRep[neighbor2].Contains(neighbor1)))
                        {

                            edgeCount++;
                        }
                    }

                   
                }

                if (!pDirected)
                {
                    edgeCount *= 2.0;
                }

                
                averageClusteringCoefficient += edgeCount / size;
            }

            return averageClusteringCoefficient / N;

        }

        public double ClusteringCoefficient(RandomNetwork network)
        {
            List<UndirectedEdge<int>> neighbors;
            double sum = 0;
            int count = 0;
            foreach (var v in network.MGraph.Vertices)
            {
                neighbors = network.MGraph.AdjacentEdges(v).ToList();
                for (int i = 0; i < neighbors.Count; i++)
                {
                    var s = neighbors.ElementAt(i);
                    for (int j = i + 1; j < neighbors.Count; j++)
                    {
                        var edgesNeighbor = network.MGraph.AdjacentEdges(s.Target).ToList();
                        if (edgesNeighbor.Count(e => e.Target == neighbors.ElementAt(j).Target) > 0)
                            count++;
                    }
                }
                if (count != 0)
                {
                    sum += count * 2.0 / (neighbors.Count * (neighbors.Count - 1));
                }
                count = 0;

            }

            return sum / network.MGraph.VertexCount;
        }
    }
}

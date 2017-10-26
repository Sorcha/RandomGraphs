using System.Collections.Generic;
using System.Linq;
using QuickGraph;

namespace RCp1.Models
{
    public class RandomModelBase
    {
        protected long NumberOfNodes;

        protected double Probability;

        public UndirectedGraph<int, UndirectedEdge<int>> Graph { get; set; }

        public RandomModelBase(long numberOfNodes)
        {
            NumberOfNodes = numberOfNodes;
        }


        public Dictionary<int, int> DegreeDistribuition()
        {
            var distribuitionDegree = new Dictionary<int, int>();
            foreach (var vertice in Graph.Vertices)
            {
                int degree = Graph.AdjacentDegree(vertice);

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

        public double ClusteringCoefficient()
        {
            List<UndirectedEdge<int>> neighbors;
            double sum = 0;
            int count = 0;
            foreach (var v in Graph.Vertices)
            {
                neighbors = Graph.AdjacentEdges(v).ToList();
                for (int i = 0; i < neighbors.Count; i++)
                {
                    var s = neighbors.ElementAt(i);
                    for (int j = i + 1; j < neighbors.Count; j++)
                    {
                        var edgesNeighbor = Graph.AdjacentEdges(s.Target).ToList();
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

            return sum / Graph.VertexCount;
        }

  

    }
}

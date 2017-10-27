using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms.ConnectedComponents;

namespace RCp1.Models
{
    public class RandomModelBase
    {
        protected long NumberOfNodes;

        protected double Probability;
        protected long Count;
        protected long MaxEdges;

        public UndirectedGraph<int, UndirectedEdge<int>> Graph { get; set; }

        public RandomModelBase(long numberOfNodes)
        {
            NumberOfNodes = numberOfNodes;
        }
        public long getCount()
        {
            return Count;
        }
        public long getMaxEdges()
        {
            return MaxEdges;
        }
        public int getGCC()
        {
            int max = 0;
            int[] maxs;
            var x = new ConnectedComponentsAlgorithm<int, UndirectedEdge<int>>(Graph);
            x.Compute();
            maxs = new int[x.ComponentCount];
            IEnumerator<KeyValuePair<int, int>> enumerator = x.Components.GetEnumerator();
            //Console.WriteLine("Value : " + enumerator.Current.Value);
            //maxs[enumerator.Current.Value]++;

            while (enumerator.MoveNext())
            {
                //Console.WriteLine("Value : " + enumerator.Current.Value);
                maxs[enumerator.Current.Value]++;
            }
            for (int i = 0; i < x.ComponentCount; i++)
            {
                if (maxs[i] > max)
                {
                    max = maxs[i];
                }
            }

            return max;
        }
        public UndirectedGraph<int, UndirectedEdge<int>> getGraph()
        {
            //g.TrimEdgeExcess();
            return Graph;
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

using QuickGraph;
using QuickGraph.Algorithms.ConnectedComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCp1
{
    public class RandomErdosGraph
    {
        private readonly int _numberOfNodes;

        private readonly double _probability;
        private int count = 0;
        private int MaxEdges = 0;
        public UndirectedGraph<int, UndirectedEdge<int>> Graph { get; private set; }

        public RandomErdosGraph(int numberOfNodes, double probability)
        {
            _numberOfNodes = numberOfNodes;

            _probability = probability;

            Graph = new UndirectedGraph<int, UndirectedEdge<int>>();

            List<int> vertexs = new List<int>();

            Random random = new Random();

            for (int i = 0; i < _numberOfNodes; i++)
            {
                var vertex = i;
                vertexs.Add(vertex);
                Graph.AddVertex(vertex);

            }

            for (int i = 0; i < _numberOfNodes; i++)
            {
                for (int j = i; j < _numberOfNodes; j++)
                {
                    if (i != j)
                    {
                        var p = random.NextDouble();
                        if (_probability == 1.0f || p >= _probability)
                        {
                            var e1 = new UndirectedEdge<int>(vertexs.ElementAt(i), vertexs.ElementAt(j));
                            Graph.AddEdge(e1);
                        }
                    }

                }

            }
        }
                public int getCount() {
            return count;
        }
        public int getMaxEdges()
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
            for(int i = 0; i < x.ComponentCount; i++)
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

    }
}

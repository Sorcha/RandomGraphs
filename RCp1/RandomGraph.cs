using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Data;
using QuickGraph.Algorithms.ConnectedComponents;
namespace RCp1
{
    class RandomGraph
    {

        UndirectedGraph<int, UndirectedEdge<int>> g = new UndirectedGraph<int, UndirectedEdge<int>>(false);
        Random random = new Random();
        int count = 0;
        int MaxEdges = 0;
        public RandomGraph(int N, double P)
        {
            int v1 = 0, v2 = 0;
            count = 0;
            MaxEdges = (int)Math.Round(P * N * (N - 1) / 2);
            for (int i = 1; i <= N; i++)
                g.AddVertex(i);
            for (int i = 0; i < MaxEdges; i++)
            {
                v1 = 0;
                v2 = 0;
                v1 = random.Next() % N + 1;
                v2 = random.Next() % N + 1;
                if (v1 == v2)
                {
                    continue;
                }
                if (!g.ContainsEdge(v2, v1))
                {
                    var e1 = new UndirectedEdge<int>(v1, v2);
                    bool v = g.AddEdge(e1);
                    if(v) count++;
                }               
            }
            Console.WriteLine("Max Edges: " + MaxEdges);
            Console.WriteLine("Egdes created: " + count);

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
            var x = new ConnectedComponentsAlgorithm<int, UndirectedEdge<int>>(g);
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
            return g;
        }
    }
}

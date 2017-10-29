using System;
using System.Collections.Generic;
using QuickGraph;
using RCp1.Data;

namespace RCp1.Models
{
    public class ErdösRenyiModel : RandomModelBase
    {
        private double _probability;

        public ErdösRenyiModel(long numberOfNodes, double probability) : base(numberOfNodes)
        {
            _probability = probability;

            Graph = new UndirectedGraph<int, UndirectedEdge<int>>();

            _probability = probability;
            MaxEdges = numberOfNodes * (numberOfNodes - 1) / 2;
            var edges = MaxEdges * probability;
            MaxEdges = (long) edges;
           // Console.WriteLine(edges);
            for (int i = 1; i <= NumberOfNodes; i++)
            {
                Graph.AddVertex(i);
            }

            if (probability.Equals(0.0)) return;

            Random rand = new Random();
            for (int i = 1; i <= NumberOfNodes; i++)
            {
                for (int j = i + 1; j <= NumberOfNodes; j++)
                {
                    //if(i==j) continue;

                    if (probability.Equals(1.0) || rand.NextDouble() <= probability)
                    {
                        Graph.AddEdge(new UndirectedEdge<int>(i, j));
                        Count++;
                    }
                }
            }
        }

       
    }
}

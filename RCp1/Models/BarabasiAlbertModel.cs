using System;
using System.Collections.Generic;
using QuickGraph;
using RCp1.Data;

namespace RCp1.Models
{
    public class BarabasiAlbertModel : RandomNetworkModel
    {
        private readonly int _initNumNodes;

  
        private readonly int _edgesToAdd;


        public BarabasiAlbertModel(int pNumNodes, bool pAllowSelfEdge,
            bool pDirected, int pInit, int pEdgesToAdd): base(pNumNodes, 0, pAllowSelfEdge, pDirected)
        {
            
            _initNumNodes = pInit;

            if (_initNumNodes > pNumNodes)
            {
                _initNumNodes = pNumNodes;
            }
            _edgesToAdd = pEdgesToAdd;
        }

        public override IRandomNetworkGenerator Copy()
        {
            return new BarabasiAlbertModel(NumNodes, AllowSelfEdge, Directed, _initNumNodes, _edgesToAdd);
        }


        public override string GetName()
        {
            return "Barabasi-Albert Model";
        }


       
        public override RandomNetwork Generate()
        {

            RandomNetwork randomNetwork = new RandomNetwork(Directed);

            randomNetwork.SetTitle(GetName());

            long time = DateTime.UtcNow.Millisecond;

            int[] nodes = new int[NumNodes];

            int []degrees = new int[NumNodes];

            for (int i = 0; i < NumNodes; i++)
            {
               
                nodes[i] = randomNetwork.NodeCreate();
            }

            NumEdges = 0;
            
            for (int i = 0; i < _initNumNodes; i++)
            {
                for (int j = (i + 1); j < _initNumNodes; j++)
                {
                    
                    randomNetwork.EdgeCreate(nodes[i], nodes[j], Directed);

                    
                    degrees[i]++;
                    degrees[j]++;
                    
                    NumEdges++;
                }
            }
            
            for (int i = _initNumNodes; i < NumNodes; i++)
            {

                int added = 0;
                double degreeIgnore = 0;
                for (int m = 0; m < _edgesToAdd; m++)
                {
                    double prob = 0;
                    double randNum = Random.NextDouble();
                    
                    for (int j = 0; j < i; j++)
                    {
                        List<UndirectedEdge<int>> edges = randomNetwork.EdgesConnecting(nodes[i], nodes[j], Directed, false, !Directed);

                        if (edges.Count==0)
                        {
                            prob += (double)((double)degrees[j])
                                / ((double)(2.0d * NumEdges) - degreeIgnore);
                        }

                      
                        if (randNum <= prob)
                        {
                           
                            randomNetwork.EdgeCreate(nodes[i], nodes[j], Directed);

                            degreeIgnore += degrees[j];

                           
                            added++;
                            degrees[i]++;
                            degrees[j]++;
                            
                            break;
                        }
                    }
                }
                NumEdges += added;
            }
            return randomNetwork;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using RCp1.Data;

namespace RCp1.Models
{
    public class WattsStrogatzModel : RandomNetworkModel
    {
        private readonly double _beta;

        
        private int _degree;

       
        public WattsStrogatzModel(int pNumNodes, bool pAllowSelfEdge,
                bool pDirected, double pBeta, int pDegree):base(pNumNodes, 0, pAllowSelfEdge, pDirected)
        {

            _degree = pDegree;
            _beta = pBeta;
        }

   
        public override IRandomNetworkGenerator Copy()
        {
            return new WattsStrogatzModel(NumNodes, AllowSelfEdge, Directed, _beta, _degree);
        }

        public override string GetName()
        {
            return "Watts-Strogatz Model";
        }

        public override RandomNetwork Generate()
        {
            
            RandomNetwork randomNetwork = new RandomNetwork(Directed);

            randomNetwork.SetTitle(GetName());
            NumEdges = 0;
            
            int[] nodes = new int[NumNodes];
            
            for (int i = 0; i < NumNodes; i++)
            {
                nodes[i] = randomNetwork.NodeCreate();
            }
            
            if (Directed && AllowSelfEdge && (2 * _degree > NumNodes))
            {
                _degree = NumNodes / 2;
            }
            else if (Directed && (2 * _degree > (NumNodes - 1) / 2))
            {
                _degree = (NumNodes - 1) / 2;
            }

            //Make sure that the degree chosen is feasbile
            if (!Directed && AllowSelfEdge && (2 * _degree > NumNodes))
            {
                _degree = NumNodes / 2;
            }
            else if (!Directed && (2 * _degree > (NumNodes - 1) / 2))
            {
                _degree = (NumNodes - 1) / 2;
            }
            
            for (int i = 0; i < NumNodes; i++)
            {

                int start = i - _degree;

                if (start < 0)
                {
                    start = NumNodes + start;
                }

                int count = 0;
                int stop = 2 * _degree;



                while (count < stop)
                {

                    if ((i != start) || (AllowSelfEdge))
                    {
                        if (((!Directed) && (nodes[i] <= nodes[start])) || (Directed))
                        {
                            randomNetwork.EdgeCreate(nodes[i], nodes[start], Directed);
                            NumEdges++;
                        }
                        if (i != start)
                        {
                            count++;
                        }
                    }
                    start = (start + 1) % NumNodes;
                }

            }
            
            LinkedList<UndirectedEdge<int>> edgeList = new LinkedList<UndirectedEdge<int>>();
            var iter = randomNetwork.Edges();
            foreach (var edge in iter)
            {
                edgeList.AddLast(edge);
            }
           
            while (edgeList.Count > 0)
            {
                var nextEdge = edgeList.ElementAt(0);
                    edgeList.RemoveFirst();
                int source = nextEdge.Source;
                int target = nextEdge.Target;

                
                double percent = Random.NextDouble();
                
                if (percent <= _beta)
                {
                    
                    int k = 0;
                    bool candidate = false;
                    
                    while (!candidate)
                    {
                        k = Math.Abs(Random.Next() % NumNodes);
                        candidate = true;
                        
                        candidate = candidate && (k != source) || (AllowSelfEdge);
                        candidate = candidate && (k != target);
                        
                        int edgeCount = 0;
                        var edgeIter = randomNetwork.EdgesAdjacent(source, Directed, false, !Directed);
                        foreach (var edge in edgeIter)
                        {
                            int neighbor = edge.Target;
                            if (neighbor == source)
                            {
                                neighbor = edge.Source;
                            }
                            
                            candidate = candidate && (neighbor != k);
                            edgeCount++;
                        }
                        
                       
                        if ((edgeCount == NumNodes) && (AllowSelfEdge))
                        {
                            candidate = false;
                            break;
                        }
                        else if ((edgeCount == (NumNodes - 1)) && (!AllowSelfEdge))
                        {
                            candidate = false;
                            break;
                        }

                    }
                    if (candidate)
                    {
                        randomNetwork.EdgeRemove(nextEdge);
                        randomNetwork.EdgeCreate(source, k, Directed);

                    }
                }
            }
            
            return randomNetwork;
        }
    }
}

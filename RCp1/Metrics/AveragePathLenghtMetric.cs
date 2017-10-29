using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCp1.Data;

namespace RCp1.Metrics
{
    public class AveragePathLenghtMetric : INetworkMetric
    {
        public string GetDisplayName()
        {
            return "Average Path Lenght";
        }

     
        public INetworkMetric Copy()
        {
            return new AveragePathLenghtMetric();
        }



        public double Analyze(RandomNetwork pNetwork, bool pDirected)
        {
            double averageShortestPath = 0;
            
            int N = pNetwork.Nodes().Count;


            int invalidPaths = 0;
            for (int i = 0; i < N; i++)
            {
                int []  distance = new int[N];
                bool[] used = new bool[N];
                
                for (int j = 0; j < N; j++)
                {
                    distance[j] = Int32.MaxValue;

                }
                var edgeIterator = pNetwork.EdgesAdjacent(i, pDirected, false, !pDirected);

                foreach (var edge in edgeIterator)
                {
                    int neighborIndex = edge.Source;
                    
                    if (neighborIndex == i)
                    {
                        neighborIndex = edge.Target;
                    }
                    
                    distance[neighborIndex] = 1;
                }
                
                
                for (int allowed = 1; allowed < N; allowed++)
                {
                    //Find the closest node
                    int min = Int32.MaxValue;
                    int index = 0;
                    for (int j = 0; j < N; j++)
                    {
                        if ((min > distance[j]) && (!used[j]))
                        {
                            min = distance[j];
                            index = j;
                        }
                    }

                    //Mark the closest node as used
                    used[index] = true;
                    
                    var adjIterator = pNetwork.EdgesAdjacent(index, pDirected, false, !pDirected);

                    foreach (var adj in adjIterator)
                    {
                        int k = adj.Source;
                        
                        if (k == index)
                        {
                            k = adj.Target;
                        }

                        if (!used[k])
                        {
                            int sum = distance[index] + 1;
                            if (sum < distance[k])
                            {
                                distance[k] = sum;
                            }
                        }
                    }
                 
                }
                
                for (int j = 0; j < N; j++)
                {
                    if (i != j)
                    {
                        if ((distance[j] < Int32.MaxValue) && (distance[j] > 0))
                        {
                            averageShortestPath += distance[j];
                        }
                        else
                        {
                            invalidPaths++;
                        }
                    }
                }
            }

            
            return averageShortestPath / ((double)(N * (N - 1.0d) - invalidPaths));
        }
    }
}

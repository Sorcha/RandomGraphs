using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms.ConnectedComponents;

namespace RCp1.Data
{
    public class RandomNetwork
    {
        private int _nodeIdCount;

        private int _edgeIdCount;

        public UndirectedGraph<int, UndirectedEdge<int>> MGraph { get; private set; }

        private string _mNetworkName;

        private readonly bool _mDirected;

        private int _mNumNodes;


        private int _mNumEdges;

        private readonly Dictionary<int, UndirectedEdge<int>> _edgeDictionary;

        public RandomNetwork(bool pDirected)
        {

            MGraph = new UndirectedGraph<int, UndirectedEdge<int>>();

            _mDirected = pDirected;

            _mNumNodes = 0;
            _mNumEdges = 0;
            _edgeIdCount = 0;
            _nodeIdCount = 0;


            _edgeDictionary = new Dictionary<int, UndirectedEdge<int>>();

        }
        public Dictionary<int,int> DegreeDistribuition()
        {
            Metrics.DegreeDistributionMetric m = new Metrics.DegreeDistributionMetric();
            return m.Analyze(this);
        }
        public double ClusteringCoefficient()
        {
            Metrics.ClusteringCoefficientMetric m = new Metrics.ClusteringCoefficientMetric();
            return m.Analyze(this, false);
        }
        public double AveragePathLength()
        {
            Metrics.AveragePathLenghtMetric m = new Metrics.AveragePathLenghtMetric();
            return m.Analyze(this, false);
        }
        public int getGCC()
        {
            int max = 0;
            int[] maxs;
            var x = new ConnectedComponentsAlgorithm<int, UndirectedEdge<int>>(MGraph);
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
        public List<int> Nodes()
        {
            return MGraph.Vertices.ToList();
        }


        public List<UndirectedEdge<int>> Edges()
        {
            return MGraph.Edges.ToList();
        }

        public int NodeCreate()
        {
            var nodeNumber = _nodeIdCount;
            _mNumNodes++;
            _nodeIdCount++;
            MGraph.AddVertex(nodeNumber);
            return nodeNumber;
        }



        public bool NodeRemove(int node)
        {
            _mNumNodes--;
            return MGraph.RemoveVertex(node);
        }

        public int EdgeCreate(int sourceNode, int targetNode, bool directed)
        {
            var edgeNumber = _edgeIdCount;
            _mNumEdges++;
            _edgeIdCount++;
            var edge = new UndirectedEdge<int>(sourceNode, targetNode);
            _edgeDictionary.Add(edgeNumber, edge);
            MGraph.AddEdge(edge);
            return edgeNumber;
        }


        public bool EdgeRemove(int edge)
        {
            _mNumEdges--;
            var edgeUn = _edgeDictionary[edge];
            _edgeDictionary.Remove(edge);
            return MGraph.RemoveEdge(edgeUn);
        }

        public bool EdgeRemove(UndirectedEdge<int> edge)
        {
            if (MGraph.ContainsEdge(edge))
            {
                _mNumEdges--;
                var findf = _edgeDictionary.Values.ToList()
                    .Find((e) =>
                    {
                        return e.Target == edge.Target && e.Source == edge.Source;
                    }
                    );
                if (_edgeDictionary.Values.ToList().Find((e) => e.Target == edge.Target && e.Source == edge.Source) != null)
                {
                    var edgeUn = _edgeDictionary.First(e => e.Value.Target == edge.Target && e.Value.Source==edge.Source);
                    if (edgeUn.Equals(default(KeyValuePair<int, UndirectedEdge<int>>)))
                    {
                        return false;
                    }
                    _edgeDictionary.Remove(edgeUn.Key);
                }
                return MGraph.RemoveEdge(edge);
            }
            
            return false;
        }


        public bool NodeExists(int node)
        {
            return MGraph.ContainsVertex(node);
        }


        public byte EdgeType(int edge)
        {
            return 0;
        }

        public int EdgeSource(int edge)
        {
            var edgeUn = _edgeDictionary[edge];
            return edgeUn.Source;
        }

        public int EdgeTarget(int edge)
        {
            var edgeUn = _edgeDictionary[edge];
            return edgeUn.Target;
        }

        public List<UndirectedEdge<int>> EdgesAdjacent(int node, bool outgoing, bool incoming,
            bool undirected)
        {
            return MGraph.AdjacentEdges(node).ToList();
        }


        public List<UndirectedEdge<int>> EdgesConnecting(int node0, int node1, bool outgoing, bool incoming,
            bool undirected)
        {

            var edgesNode0AndNode1 = MGraph.Edges.Where((e) => e.Source == node0 && e.Target == node1);
            var edgesNode1AndNode0 = MGraph.Edges.Where((e) => e.Source == node1 && e.Target == node0);

            return edgesNode1AndNode0.Concat(edgesNode0AndNode1).ToList();

        }

        public bool GetDirected()
        {
            return _mDirected;
        }



        public void SetTitle(string pNetworkName)
        {
            _mNetworkName = pNetworkName;
        }

        public string GetTitle()
        {
            return _mNetworkName;
        }


        public int GetNumNodes()
        {
            return _mNumNodes;
        }

        public int GetNumEdges()
        {
            return _mNumEdges;
        }


    }
}

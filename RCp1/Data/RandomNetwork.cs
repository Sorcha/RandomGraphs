using System.Collections.Generic;
using System.Linq;
using QuickGraph;

namespace RCp1.Data
{
    public class RandomNetwork
    {
        private int _nodeIdCount;

        private int _edgeIdCount;

        private readonly UndirectedGraph<int, UndirectedEdge<int>> _mGraph;

        private string _mNetworkName;

        private readonly bool _mDirected;

        private int _mNumNodes;


        private int _mNumEdges;

        private readonly Dictionary<int, UndirectedEdge<int>> _edgeDictionary;

        public RandomNetwork(bool pDirected)
        {

            _mGraph = new UndirectedGraph<int, UndirectedEdge<int>>();

            _mDirected = pDirected;

            _mNumNodes = 0;
            _mNumEdges = 0;
            _edgeIdCount = 0;
            _nodeIdCount = 0;


            _edgeDictionary = new Dictionary<int, UndirectedEdge<int>>();

        }

        public List<int> Nodes()
        {
            return _mGraph.Vertices.ToList();
        }


        public List<UndirectedEdge<int>> Edges()
        {
            return _mGraph.Edges.ToList();
        }

        public int NodeCreate()
        {
            var nodeNumber = _nodeIdCount;
            _mNumNodes++;
            _nodeIdCount++;
            _mGraph.AddVertex(nodeNumber);
            return nodeNumber;
        }



        public bool NodeRemove(int node)
        {
            _mNumNodes--;
            return _mGraph.RemoveVertex(node);
        }

        public int EdgeCreate(int sourceNode, int targetNode, bool directed)
        {
            var edgeNumber = _edgeIdCount;
            _mNumEdges++;
            _edgeIdCount++;
            var edge = new UndirectedEdge<int>(sourceNode, targetNode);
            _edgeDictionary.Add(edgeNumber, edge);
            _mGraph.AddEdge(new UndirectedEdge<int>(sourceNode, targetNode));
            return edgeNumber;
        }


        public bool EdgeRemove(int edge)
        {
            _mNumEdges--;
            var edgeUn = _edgeDictionary[edge];
            _edgeDictionary.Remove(edge);
            return _mGraph.RemoveEdge(edgeUn);
        }

        public bool EdgeRemove(UndirectedEdge<int> edge)
        {
            _mNumEdges--;
            var edgeUn = _edgeDictionary.First(e => e.Value==edge);
            _edgeDictionary.Remove(edgeUn.Key);
            return _mGraph.RemoveEdge(edge);
        }


        public bool NodeExists(int node)
        {
            return _mGraph.ContainsVertex(node);
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
            return _mGraph.AdjacentEdges(node).ToList();
        }


        public List<UndirectedEdge<int>> EdgesConnecting(int node0, int node1, bool outgoing, bool incoming,
            bool undirected)
        {

            var edgesNode0AndNode1 = _mGraph.Edges.Where((e) => e.Source == node0 && e.Target == node1);
            var edgesNode1AndNode0 = _mGraph.Edges.Where((e) => e.Source == node1 && e.Target == node0);

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

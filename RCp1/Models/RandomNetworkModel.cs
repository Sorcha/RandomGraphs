using System;
using RCp1.Data;
using RCp1.Generators;

namespace RCp1.Models
{
    public abstract class RandomNetworkModel : IRandomNetworkGenerator
    {
        protected int NumNodes;
        
        protected int NumEdges;
        
        protected bool Directed;
        protected bool AllowSelfEdge;


        protected int Seed;

     
        protected Random Random;


        protected RandomNetworkModel(int pNumNodes, int pNumEdges, bool pAllowSelfEdge,
              bool pDirected)
        {
            NumNodes = pNumNodes;
            NumEdges = pNumEdges;
            Directed = pDirected;
            AllowSelfEdge = pAllowSelfEdge;
            Seed = DateTime.UtcNow.Millisecond;
            Random = new Random(Seed);
        }

        public void SetSeed(int pSeed)
        {
            Seed = pSeed;
            Random = new Random(Seed);
        }

   
        public long GetSeed()
        {
            return Seed;
        }

        public int GetNumNodes()
        {
            return NumNodes;
        }

      
        public int GetNumEdges()
        {
            return NumEdges;
        }


        public bool GetDirected()
        {
            return Directed;
        }

        public abstract RandomNetwork Generate();

        public abstract string GetName();

        public abstract IRandomNetworkGenerator Copy();
    }
}

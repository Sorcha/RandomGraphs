using System;
using RCp1.Data;

namespace RCp1.Models
{
    public abstract class NetworkRandomizerModel : IRandomNetworkGenerator
    {

        protected int MSeed;

        protected Random MRandom;


        protected bool MDirected;

        protected RandomNetwork MOriginal;



        NetworkRandomizerModel(RandomNetwork pRandomNetwork, bool pDirected)
        {
            MDirected = pDirected;
            MOriginal = pRandomNetwork;
            MSeed = DateTime.UtcNow.Millisecond;
            MRandom = new Random(MSeed);
        }

        public void SetSeed(int pSeed)
        {
            MSeed = pSeed;
            MRandom = new Random(MSeed);
        }


        public long GetSeed()
        {
            return MSeed;
        }

        public bool GetDirected()
        {
            return MDirected;
        }

        public RandomNetwork GetOriginal()
        {
            return MOriginal;
        }


        public abstract RandomNetwork Generate();

        public abstract string GetName();

        public abstract IRandomNetworkGenerator Copy();
    }
}

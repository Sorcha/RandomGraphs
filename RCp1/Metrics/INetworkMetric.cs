using RCp1.Data;

namespace RCp1.Metrics
{
    public interface INetworkMetric
    {

        double Analyze(RandomNetwork pNetwork, bool pDirected);

        string GetDisplayName();


        INetworkMetric Copy();

    }
}

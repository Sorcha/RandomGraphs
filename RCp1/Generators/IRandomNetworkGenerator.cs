using RCp1.Data;

namespace RCp1.Generators
{
    public interface IRandomNetworkGenerator
    {

        RandomNetwork Generate();

        string GetName();


        IRandomNetworkGenerator Copy();
    }
}

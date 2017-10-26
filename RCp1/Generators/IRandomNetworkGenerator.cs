using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCp1.Data;

namespace RCp1.Models
{
    public interface IRandomNetworkGenerator
    {

        RandomNetwork Generate();

        string GetName();


        IRandomNetworkGenerator Copy();
    }
}

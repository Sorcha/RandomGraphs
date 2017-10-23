using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RCp1
{
    public sealed class FileDotEngine : IDotEngine
    {
        public string Run(GraphvizImageType imageType, string dot, string outputFileName)
        {
            string output = outputFileName;
            dot = dot.Replace('>', '-');
            File.WriteAllText(output, dot);
            ProcessStartInfo startInfo = new ProcessStartInfo("dot.exe");
            startInfo.Arguments = string.Format(@"-Tpng {0} -o {0}.png", output);
            var process = Process.Start(startInfo);
            process.WaitForExit();
            return output;
        }
    }
}

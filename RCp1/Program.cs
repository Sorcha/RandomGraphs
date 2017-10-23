using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Graphviz;
using System.Windows.Forms;

namespace RCp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RCP1());
            
            //for (int i = 1; i < 10; i++)
            //{

           /* int N = 20;
                double p = 1.0 / N;
                Console.WriteLine(N + " "+ p);
                RandomGraph g = new RandomGraph(N, p);
            
                var graphviz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(g.getGraph());
                string output = graphviz.Generate(new FileDotEngine(), "C:/temp/graph");
            //}
            */
            
        }
    }
}

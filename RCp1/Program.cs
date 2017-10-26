using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Graphviz;
using System.Windows.Forms;
using RCp1.Data;
using RCp1.Metrics;
using RCp1.Models;

namespace RCp1
{
    class Program
    {
        static void Main(string[] args)
        {
            testBarabasiAlbertModel();
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

        public static void testBarabasiAlbertModel()
        {

            //Model specific variabels
            int nodes = 50;
            int degree = 2;
            bool allowReflexive = false;
            bool directed = false;
            int init = 3;
            int edges = ((init) * (init - 1) / 2 + (degree * (nodes - init)));

            //Create the model
            BarabasiAlbertModel bam;


            for (int j = 4; j <= 10; j += 2)
            {
                bam = new BarabasiAlbertModel(j, allowReflexive, directed, init, degree);

                RandomNetwork random_network = bam.Generate();
                DegreeDistributionMetric metric = new DegreeDistributionMetric();
                AverageDegreeMetric m = new AverageDegreeMetric();
                m.Analyze(random_network, directed);
                double c = metric.Analyze(random_network, directed);
                
            }


        }
    }
}

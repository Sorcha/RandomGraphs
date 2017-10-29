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
            //testWattsStrogatzModel();
            testBaModel();
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
            int nodes = 500;
            int degree = 2;
            bool allowReflexive = false;
            bool directed = false;
            int init = 3;
            int edges = ((init) * (init - 1) / 2 + (degree * (nodes - init)));

            //Create the model
            BarabasiAlbertModel bam;
            DegreeDistributionMetric metric = new DegreeDistributionMetric();

            for (int i =100; i < 1000; i+=100)
            {
                bam = new BarabasiAlbertModel(i, allowReflexive, directed, init, degree);
                RandomNetwork random_network = bam.Generate();
                var dddegree = metric.Analyze(random_network,directed);
                Console.WriteLine(string.Format("{0};{1}", i, dddegree));
            }


            //for (int j = 4; j <= 10; j += 2)
            //{
            //    bam = new BarabasiAlbertModel(j, allowReflexive, directed, init, degree);

            //    RandomNetwork random_network = bam.Generate();
            //    DegreeDistributionMetric metric = new DegreeDistributionMetric();
            //    AverageDegreeMetric m = new AverageDegreeMetric();
            //    m.Analyze(random_network, directed);
            //    double c = metric.Analyze(random_network, directed);

            //}


        }

        public static void testWattsStrogatzModel()
        {

            int nodes = 10000;
            double beta = 0.7;
            int degree = 4;
            bool allowReflexive = false;
            bool directed = false;

            ErdösRenyiModel wsm = new ErdösRenyiModel(nodes,beta);
            
            DegreeDistributionMetric metric = new DegreeDistributionMetric();
            var dddegree = wsm.DegreeDistribuition();

            foreach (var d in dddegree)
            {
                Console.WriteLine(string.Format("{0};{1}",d.Key,d.Value));
            }

            //var graphviz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(random_network.MGraph);
            //string output = graphviz.Generate(new FileDotEngine(), "graph");

            int edges = 2 * degree * nodes;

            

        }

        public static void testBaModel()
        {

            int nodes = 1000;
            double beta = 0.7;
            int degree = 4;
            bool allowReflexive = false;
            bool directed = false;

            BarabasiAlbertModel wsm = new BarabasiAlbertModel(nodes,allowReflexive,directed,3,5);
            var net = wsm.Generate();
            DegreeDistributionMetric metric = new DegreeDistributionMetric();
            var dddegree = metric.Analyze(net);

            foreach (var d in dddegree)
            {
                Console.WriteLine(string.Format("{0};{1}", d.Key, d.Value));
            }

            //var graphviz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(random_network.MGraph);
            //string output = graphviz.Generate(new FileDotEngine(), "graph");

            int edges = 2 * degree * nodes;



        }

        public static void testWattsStrogatzModelClust()
        {
            int nodes = 100;
            double beta = 0.1;
            int degree = 4;
            bool allowReflexive = false;
            bool directed = false;

            for (double i = 0.0; i <= 1; i+=0.1)
            {
                WattsStrogatzModel wsm = new WattsStrogatzModel(nodes, allowReflexive, directed, i, degree);
                RandomNetwork random_network = wsm.Generate();
                ClusteringCoefficientMetric m = new ClusteringCoefficientMetric();
                double d = m.Analyze(random_network, directed);
                Console.Write(d+" ");
            }

            Console.WriteLine();


        }
    }
}

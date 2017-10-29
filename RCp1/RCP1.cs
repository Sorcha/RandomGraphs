using QuickGraph;
using QuickGraph.Graphviz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using RCp1.Models;

namespace RCp1
{
    public partial class RCP1 : Form
    {
        public bool visualize = true;
        public RCP1()
        {
            InitializeComponent();
  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int N = int.Parse(textBox1.Text);
            int k = int.Parse(textBox5.Text);
            //int iter = N;
            double p = 0.0;
            double.TryParse(textBox2.Text,NumberStyles.Any,CultureInfo.InvariantCulture,out p);
            WattsStrogatzModel g = new WattsStrogatzModel(N, false, false, p, k);
            Data.RandomNetwork d = g.Generate();
            if (radioButton4.Checked)
            {
                if (visualize)
                {
                    var graphviz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(d.MGraph);
                    string output = graphviz.Generate(new FileDotEngine(), "graph");
                    pictureBox1.ImageLocation = "graph.png";
                }
                label4.Text = string.Format(@"{0}", d.ClusteringCoefficient());
                var degreeDistribuition = d.DegreeDistribuition();


                ErdosDistribuitionDegreeChart.Titles.Add(new Title(RandomGraphStrings.DegreeChartTitle));

                GenerateDegreeDistribuitionChart(chart2, degreeDistribuition);
            }
            if (radioButton3.Checked)
            {
                GenerateAveragePathLengthWattsChart(chart4, N, k);
                GenerateClusteringCoefficientChart(chart3, N, k);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int N = int.Parse(textBox4.Text);
            int N_init = int.Parse(textBox6.Text);
            int E = int.Parse(textBox3.Text); ;
            BarabasiAlbertModel g = new BarabasiAlbertModel(N, false, false, N_init, E);
            Data.RandomNetwork d = g.Generate();
            if (radioButton6.Checked)
            {
                if (visualize)
                {
                    var graphviz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(d.MGraph);
                    string output = graphviz.Generate(new FileDotEngine(), "graph");
                    pictureBox2.ImageLocation = "graph.png";
                }
                label4.Text = string.Format(@"{0}", d.ClusteringCoefficient());
                var degreeDistribuition = d.DegreeDistribuition();


                ErdosDistribuitionDegreeChart.Titles.Add(new Title(RandomGraphStrings.DegreeChartTitle));

                GenerateDegreeDistribuitionChart(chart1, degreeDistribuition);
            }
            if (radioButton5.Checked)
            {
                GenerateAveragePathLengthBarabasiChart(chart7, N, N_init, E);
                GenerateClusteringCoefficientChartBarabasi(chart6, N, N_init, E);
            }
        }
        private void ErdosGenerateButton_Click(object sender, EventArgs e)
        {
            int.TryParse(ErdosNumberOfNodesTextBox.Text, out var numberOfNodes);
            int N = numberOfNodes;
            double.TryParse(ErdosProbabilityTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var probability);
            if (radioButton1.Checked)
            {

                var model = new ErdösRenyiModel(numberOfNodes, probability);
                if (visualize)
                {
                    var graphviz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(model.Graph);

                    string output = graphviz.Generate(new FileDotEngine(), "graph");

                    ErdosGraphPictureBox.ImageLocation = @"graph.png";
                }
                double avg_k = (double)2 * model.getCount() / numberOfNodes; ;
                double expected_avg_k = probability * (numberOfNodes - 1);
                label18.Text = string.Format(@"{0}", avg_k);
                label13.Text = string.Format(@"{0}", expected_avg_k);
                label15.Text = string.Format(@"{0}", model.getGCC());
                // label3.Text = string.Format(@"{0}", Metrics.ClusteringCoefficientMetric.Analyze(model, false));


                var degreeDistribuition = model.DegreeDistribuition();

                //foreach (var degree in degreeDistribuition)
                //{
                //  Console.WriteLine($@"{degree.Key} - {degree.Value}");
                //}

                ErdosDistribuitionDegreeChart.Titles.Add(new Title(RandomGraphStrings.DegreeChartTitle));

                GenerateDegreeDistribuitionChart(ErdosDistribuitionDegreeChart, degreeDistribuition);
            }
            if (radioButton2.Checked)
            {
                GenerateCriticalPointChart(chart5, N);
            }
        }
        private void RunReport(Chart chart)
        {
            // Clear Chart
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();

            // Build Chart
            //ChartSeries(datax, datay, "<k> = Ng/N", iter, N);
            //ChartAreas("Video");
            //ChartTitle("Video");
        }
        /// <summary>
        /// Sets up the look and style of the chart, Areas.
        /// </summary>
        /// <param name="title">Title of the chart.</param>
        private void ChartAreas(Chart chart, string title, double ymax)
        {
            var axisX = new Axis
            {
                //Minimum = 0,
                //Maximum = 6,
                //Interval = 1,
            };

            var axisY = new Axis
            {
                Minimum = 0,
                Maximum = ymax,
                Title = title,
            };
 
            var chartArea1 = new ChartArea
            {
                AxisX = axisX,
                AxisY = axisY,
            };
            //chartArea1.AxisX.ScaleView.Zoom(0 , xmax);
            //chartArea1.AxisY.ScaleView.Zoom(0, 1);
            //chartArea1.AxisX.ScaleView.Zoomable = true;

            chart.ChartAreas.Add(chartArea1);
        }

        /// <summary>
        /// Sets up the look and style of the chart, Title.
        /// </summary>
        /// <param name="title">Title of the chart.</param>
        private void ChartTitle(Chart chart, string title)
        {
            var titles1 = new Title
            {
                Name = title,
                Text = title + "Graph Data",
                Visible = true,
            };
            chart.Titles.Add(titles1);
        }

        /// <summary>
        /// Sets up the look and style of the chart, Legends.
        /// </summary>
        /// <param name="name">Name of the chart data.</param>
        private void ChartLegends(Chart chart, string name)
        {
            var legends1 = new Legend
            {
                Name = name,
            };
            chart.Legends.Add(legends1);
        }

        /// <summary>
        /// Sets up the look and style of the chart, Series.
        /// </summary>
        /// <param name="datax">The data type.</param>
        /// <param name="name">The name of the data.</param>
        private void ChartSeries(Chart chart, double[] datax, double[] datay, string name, int iter, int N)
        {
            var series1 = new Series
            {
                Name = name,
                Color = Color.Blue,
                //BorderWidth = 5,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Point,
            };

            for (int i = 0; i < iter; i++)
            {
                //Console.WriteLine(""+datax[i]+" , "+ datay[i]);
                if(datay[i] != 0)
                    series1.Points.AddXY(datax[i], datay[i]);
            }
            chart.Series.Add(series1);
            ChartLegends(chart, name);
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void GenerateClusteringCoefficientChartBarabasi(Chart chart, int N, int Ninit , int Edges)
        {
            int iter = 20;
            int portion = Edges;
            double[] datax = new double[iter];
            double[] datay = new double[iter];
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            BarabasiAlbertModel g;
            for (int i = 0; i < iter; i++)
            {
                //RandomGraph g = new RandomGraph(N, (double)i/iter);
                g = new BarabasiAlbertModel(N, false, false, Ninit , Edges*i);

                //ErdösRenyiModel g = new ErdösRenyiModel(N, (double)i / iter);
                Data.RandomNetwork d = g.Generate();

                datay[i] = d.ClusteringCoefficient();
                datax[i] = (double)i / iter;
                //Console.WriteLine("x : " + datax[i] + ", y : " + datay[i]);
            }
            ChartSeries(chart, datax, datay, "p = Clust. Coeff.", iter, N);
            ChartAreas(chart, "Graph Clustering Coefficient", 1);
            ChartTitle(chart, "");
        }
        private void GenerateClusteringCoefficientChart(Chart chart, int N, int k)
        {
            int iter = 20;
            double[] datax = new double[iter];
            double[] datay = new double[iter];
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            WattsStrogatzModel g;
            for (int i = 0; i < iter; i++)
            {
                //RandomGraph g = new RandomGraph(N, (double)i/iter);
                g = new WattsStrogatzModel(N, false, false, (double) i / iter, k);

                //ErdösRenyiModel g = new ErdösRenyiModel(N, (double)i / iter);
                Data.RandomNetwork d = g.Generate();
                
                datay[i] = d.ClusteringCoefficient();
                datax[i] =(double) i / iter;
                //Console.WriteLine("x : " + datax[i] + ", y : "+ datay[i]);
            }
            ChartSeries(chart, datax, datay, "p = Clust. Coeff.", iter, N);
            ChartAreas(chart, "Graph Clustering Coefficient", 1);
            ChartTitle(chart, "");
        }
        private void GenerateCriticalPointChart(Chart chart, int N)
        {
            int iter = 100;
            double max = (double) 5 / N;
            double max_portion = max / iter;
            double[] datax = new double[iter];
            double[] datay = new double[iter];
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            for (int i = 0; i < iter; i++)
            {
                //RandomGraph g = new RandomGraph(N, (double)i/iter);
                ErdösRenyiModel g = new ErdösRenyiModel(N, (double) max_portion*i);

                if ((double)2 * g.getCount() / N > 5)
                {
                    break;
                }
                datay[i] = (double)g.getGCC() / N;
                datax[i] = (double)2 * g.getCount() / N;
            }
            ChartSeries(chart, datax, datay, "<k> = Ng/N", iter, N);
            ChartAreas(chart, "Ng/N",5);
            ChartTitle(chart, "");
        }
        private void GenerateAveragePathLengthWattsChart(Chart chart, int N, int k)
        {
            int iter = 20;
            double max = 0;
            double[] datax = new double[iter];
            double[] datay = new double[iter];
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            WattsStrogatzModel g;
            for (int i = 0; i < iter; i++)
            {
                g = new WattsStrogatzModel(N, false, false, (double)i / iter, k);

                Data.RandomNetwork d = g.Generate();
                datay[i] = d.AveragePathLength();
                datax[i] = (double)i / iter;
                if (datay[i] > max) max = datay[i];
                Console.WriteLine("x : " + datax[i] + ", y : " + datay[i]);
            }
            ChartSeries(chart, datax, datay, "p = Avg Path Len", iter, N);
            ChartAreas(chart, "Graph Avg Path Length", max);
            ChartTitle(chart, "");
        }
        private void GenerateAveragePathLengthBarabasiChart(Chart chart, int N, int Ninit, int Edges)
        {
            int iter = 20;
            double max = 0;
            double[] datax = new double[iter];
            double[] datay = new double[iter];
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();
            chart.ChartAreas.Clear();
            BarabasiAlbertModel g;
            for (int i = 0; i < iter; i++)
            {
                g = new BarabasiAlbertModel(N, false, false, Ninit, Edges * i);

                Data.RandomNetwork d = g.Generate();
                datay[i] = d.AveragePathLength();
                datax[i] = (double)i / iter;
                if (datay[i] > max) max = datay[i];
                Console.WriteLine("x : " + datax[i] + ", y : " + datay[i]);
            }
            ChartSeries(chart, datax, datay, "p = Avg Path Len", iter, N);
            ChartAreas(chart, "Graph Avg Path Length", max);
            ChartTitle(chart, "");
        }
        private void GenerateDegreeDistribuitionChart(Chart chart,
            Dictionary<int,int> distribuitionDegree)
        {
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(new Title(RandomGraphStrings.DegreeChartTitle));

            var axisX = new Axis
            {
                Title = "K"
            };

            var axisY = new Axis
            {
                Title = "Sum K",
            };

            var chartArea = new ChartArea
            {
                AxisX = axisX,
                AxisY = axisY,
            };

            chart.ChartAreas.Add(chartArea);

            var serie = new Series
            {
                Name = "Name",
                Color = Color.Blue,
                //BorderWidth = 5,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Point,
            };

            for (int i = 0; i < distribuitionDegree.Keys.Max()+1; i++)
            {
                if (distribuitionDegree.Keys.Contains<int>(i))
                {
                    serie.Points.AddXY(i, distribuitionDegree[i]);
                    //Console.WriteLine("x : " + i + ", y : " + distribuitionDegree[i]);
                }
                else
                {
                    serie.Points.AddXY(i, 0);
                }

                //Console.WriteLine("x : " + degree.Key + ", y : " + degree.Value);

            }

            chart.Series.Add(serie);
        }

        private void ChangeChartSeries(Chart chart,
            double[] datax, 
            double[] datay, 
            string name, 
            int iter)
        {
            var serie = new Series
            {
                Name = name,
                Color = Color.Blue,
                BorderWidth = 5,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line,
            };

            for (int i = 0; i < iter; i++)
            {
                serie.Points.AddXY(datax[i], datay[i]);
            }

            chart.Series.Add(serie);
            //ChartLegends(name);
        }

        private void visualizeGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (visualize) {
                visualizeGraphToolStripMenuItem.Text = "Visualize Graph";
                visualize = false;
            }
            else {
                visualizeGraphToolStripMenuItem.Text = "Dont show Graph";
                visualize = true;
            }
        }


    }
}

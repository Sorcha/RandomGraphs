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

namespace RCp1
{
    public partial class RCP1 : Form
    {
        public RCP1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int N = int.Parse(textBox1.Text);
            double p = 0.0;
            double.TryParse(textBox2.Text,NumberStyles.Any,CultureInfo.InvariantCulture,out p);
            Console.WriteLine("Vertexs: " + N + " " + "Probability: " + p);
            RandomGraph g = new RandomGraph(N, p);

            var graphviz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(g.getGraph());
            string output = graphviz.Generate(new FileDotEngine(), "graph");
            pictureBox1.ImageLocation = "graph.png";
            double avg_k = (double)2*g.getCount() / N;
            double expected_avg_k = (double)2 * g.getMaxEdges() / N;
            label4.Text = string.Format(@"{0}", avg_k);
            label8.Text = string.Format(@"{0}", expected_avg_k);
            label6.Text = string.Format(@"{0}", g.getGCC());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int N = int.Parse(textBox4.Text);
            int iter = int.Parse(textBox3.Text);
            double[] datax = new double[iter];
            double[] datay = new double[iter];
            chart1.Series.Clear();
            chart1.Legends.Clear();
            chart1.Titles.Clear();
            chart1.ChartAreas.Clear();

            //  chart1.MinimumSize = Size.
            for (int i = 0; i < iter; i++)
            {
                RandomGraph g = new RandomGraph(N, (double)i/iter);
                if ((double)2 * g.getCount() / N > 6) {
                    break;
                }
                datay[i] = (double) g.getGCC()/N;
                datax[i] = (double)2 * g.getCount() / N;
                //Console.WriteLine(g.getGCC());
            }
            ChartSeries(datax, datay, "<k> = Ng/N", iter, N);
            ChartAreas("Ng/N");
            ChartTitle("");
            //chart1.Invalidate();


        }
        private void RunReport()
        {
            // Clear Chart
            chart1.Series.Clear();
            chart1.Legends.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();

            // Build Chart
            //ChartSeries(datax, datay, "<k> = Ng/N", iter, N);
            //ChartAreas("Video");
            //ChartTitle("Video");
        }
        /// <summary>
        /// Sets up the look and style of the chart, Areas.
        /// </summary>
        /// <param name="title">Title of the chart.</param>
        private void ChartAreas(string title)
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
                Maximum = 1,
                Title = title,
            };

            var chartArea1 = new ChartArea
            {
                AxisX = axisX,
                AxisY = axisY,
            };

            chart1.ChartAreas.Add(chartArea1);
        }

        /// <summary>
        /// Sets up the look and style of the chart, Title.
        /// </summary>
        /// <param name="title">Title of the chart.</param>
        private void ChartTitle(string title)
        {
            var titles1 = new Title
            {
                Name = title,
                Text = title + "Graph Data",
                Visible = true,
            };
            chart1.Titles.Add(titles1);
        }

        /// <summary>
        /// Sets up the look and style of the chart, Legends.
        /// </summary>
        /// <param name="name">Name of the chart data.</param>
        private void ChartLegends(string name)
        {
            var legends1 = new Legend
            {
                Name = name,
            };
            chart1.Legends.Add(legends1);
        }

        /// <summary>
        /// Sets up the look and style of the chart, Series.
        /// </summary>
        /// <param name="datax">The data type.</param>
        /// <param name="name">The name of the data.</param>
        private void ChartSeries(double[] datax, double[] datay, string name, int iter, int N)
        {
            var series1 = new Series
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
                series1.Points.AddXY(datax[i], datay[i]);
            }
            chart1.Series.Add(series1);
            ChartLegends(name);
        }
    }
}

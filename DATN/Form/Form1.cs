using DATN.Controller;
using DATN.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DATN.DATNDataSet;

namespace DATN
{
    public partial class Form1 : Form
    {
        BBRController controller = new BBRController();
        INTOPKController intopkController = new INTOPKController();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dATNDataSet1.DataPoint' table. You can move, or remove it, as needed.
            this.dataPointTableAdapter.Fill(this.dATNDataSet1.DataPoint);
            // TODO: This line of code loads data into the 'dATNDataSet.WeightVector' table. You can move, or remove it, as needed.
            this.weightVectorTableAdapter.Fill(this.dATNDataSet.WeightVector);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                DataPoint point = new DataPoint(Double.Parse(this.rating.Text), Double.Parse(this.star.Text));
                HashSet<WeightVector> result=controller.BBR(point,Int32.Parse(this.rank.Text));
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                if (result.Count == 0)
                {
                    MessageBox.Show("None of the weighting vectors of mV belongs to the reverse top-2 result set of q. Excute Time : "+elapsedMs);
                }
                else
                {
                    MessageBox.Show("Got something. Excute Time : " + elapsedMs);
                }
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}

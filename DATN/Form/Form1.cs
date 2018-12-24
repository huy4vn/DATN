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
        RTAController rtaController = new RTAController();
        List<DataPoint> listS;
        List<WeightVector> listW;
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

            WeightVectorEntities entities = new WeightVectorEntities();
            DATNEntities entities2 = new DATNEntities();
            listS=GetS(entities2);
            listW = GetW(entities);
            entities.Dispose();
            entities2.Dispose();
            controller.listItem = listW;
            controller.getTree();
            intopkController.listItem = listS;
            intopkController.getTree();
            controller.setIntopController(intopkController);
        }
        private void fillTable(HashSet<WeightVector> result)
        {
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("rating", typeof(float));
            table.Columns.Add("star", typeof(float));

            foreach(var item in result)
            {
                table.Rows.Add(item.id, item.rating, item.star);
            }
            this.dataGridView2.DataSource = table;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                DataPoint point = new DataPoint(Double.Parse(this.rating.Text), Double.Parse(this.star.Text));
                KeyValuePair<int, HashSet<WeightVector>> result =controller.BBR(point,Int32.Parse(this.rank.Text));
                watch.Stop();
                fillTable(result.Value);
                var elapsedMs = watch.ElapsedMilliseconds;
                chart1.Series[0].Points.AddY(elapsedMs);
                chart2.Series[0].Points.AddY(result.Key);
                MessageBox.Show("Done");

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public List<WeightVector> GetW(WeightVectorEntities entities)
        {
            var query = from p in entities.WeightVectors 
                        select p;
            return query.ToList();
        }
        public List<DataPoint> GetS(DATNEntities entities)
        {
            var query = from p in entities.DataPoints
                        select p;

            return query.ToList();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            DataPoint point = new DataPoint(Double.Parse(this.rating.Text), Double.Parse(this.star.Text));
           
            KeyValuePair<int,HashSet<WeightVector>> result =rtaController.RTA(listS,listW,point, Int32.Parse(this.rank.Text));
            watch.Stop();
            fillTable(result.Value);
            var elapsedMs = watch.ElapsedMilliseconds;
            chart1.Series[1].Points.AddY(elapsedMs);
            chart2.Series[1].Points.AddY(result.Key);
            MessageBox.Show("Done");
        }
    }
}

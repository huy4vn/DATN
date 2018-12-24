using DATN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Controller
{
    class RTAController
    {
        public double f(DataPoint q,WeightVector w)
        {
            return q.star.Value * w.star.Value + q.rating.Value * w.rating.Value;
        }
        public double max(List<DataPoint> list,WeightVector w)
        {

            double Max = Double.MinValue;
            if (w == null) return Max;
            foreach(var item in list)
            {
                double temp = f(item, w);
                if (temp > Max)
                {
                    Max = temp;
                }
            }
            return Max;
        }
        public KeyValuePair<int,HashSet<WeightVector>> RTA(List<DataPoint> S, List<WeightVector>W, DataPoint q, int k)
        {
            HashSet<WeightVector> w2 = new HashSet<WeightVector>();
            List<DataPoint> buffer = new List<DataPoint>(k);
            int totalAccessWeightVector=0;
            double threshold = Int32.MaxValue;
            for (var i = 0; i < W.Count; i++)         {
                totalAccessWeightVector++;
                if (f(q, W[i]) <= threshold)
                {
                    buffer = TopKController.TopK(W[i], S,k);
                    if (f(q, W[i]) <= max(buffer, W[i]))
                    {
                        w2.Add(W[i]);
                    }
                }
                threshold = max(buffer, i+1<W.Count?W[i + 1]:null);
            }
            return new KeyValuePair<int, HashSet<WeightVector>>(totalAccessWeightVector,w2);
        }
    }
}

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

        internal List<WeightVector> SortList(List<WeightVector> listW)
        {
            List<WeightVector> listResult = new List<WeightVector>();

            List<WeightVector> listWTemp = new List<WeightVector>();
            listWTemp.AddRange(listW);
            WeightVector similarW = new WeightVector(0.5, 0.5);
            int totalW = listWTemp.Count;
            while (listResult.Count< totalW)
            {
                double max = Int32.MinValue;
                int index = -1;
                for (var i= 0;i < listWTemp.Count;i++)
                {
                    WeightVector itemW = listWTemp[i];
                    double similarValue = GetCosineSimilarity(itemW, similarW);
                    if (similarValue > max)
                    {
                        index = i;
                        max = similarValue;
                    }
                }
                similarW = listWTemp[index];
                listResult.Add(similarW);
                listWTemp.RemoveAt(index);
            }
            return listResult;

        }
        public static double GetCosineSimilarity(WeightVector V1, WeightVector V2)
        {
            return (V1.rating.Value * V2.rating.Value + V1.star.Value * V2.star.Value) / (Math.Sqrt(Math.Pow(V1.rating.Value, 2) + Math.Pow(V1.star.Value, 2))*
                       Math.Sqrt(Math.Pow(V2.rating.Value, 2) + Math.Pow(V2.star.Value, 2)));
        }
    }
}

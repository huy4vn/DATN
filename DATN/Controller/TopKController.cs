using DATN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Controller
{
    class TopKController
    {
        public static double f(DataPoint q, WeightVector w)
        {
            return q.star.Value * w.star.Value + q.rating.Value * w.rating.Value;
        }

        public static List<DataPoint> TopK(WeightVector w,List<DataPoint> points,int rank)
        {
            List<DataPoint> buffer = points;
            buffer.Sort((x, y) => f(x, w).CompareTo(f(y, w)));
            return buffer.GetRange(0,rank);
        }
    }
}

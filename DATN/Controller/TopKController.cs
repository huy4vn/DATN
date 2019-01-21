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
        public static List<DataPoint> TopK(WeightVector w,List<DataPoint> points,int rank)
        {
            List<DataPoint> buffer = points;
            buffer.Sort((x, y) => BasicFunction.f(x, w).CompareTo(BasicFunction.f(y, w)));
            return buffer.GetRange(0,rank);
        }
    }
}

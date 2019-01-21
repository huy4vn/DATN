using DATN.Model;
using RTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Controller
{
    class BasicFunction
    {
        public static double f(DataPoint q, WeightVector w)
        {
            return q.star.Value * w.star.Value + q.rating.Value * w.rating.Value;
        }

        public static double uv(MBRModel<WeightVector> mv, DataPoint p)
        {
            return (double)(mv.upperRight.rating * p.rating) + (double)(mv.upperRight.star * p.star);
        }
        public static double lv(MBRModel<WeightVector> mv, DataPoint p)
        {
            return (double)(mv.lowerLeft.rating * p.rating) + (double)(mv.lowerLeft.star * p.star);
        }

        public static bool isDataPoint(MBRModel<WeightVector> point)
        {
            return point.lowerLeft.rating == point.upperRight.rating && point.lowerLeft.star == point.upperRight.star;
        }
        public static bool isDataPoint(MBRModel<DataPoint> point)
        {
            return point.lowerLeft.rating == point.upperRight.rating && point.lowerLeft.star == point.upperRight.star;
        }

        public static double GetCosineSimilarity(MBRModel<WeightVector> V1, MBRModel<WeightVector> V2, DataPoint p)
        {
            return (lv(V1, p) * lv(V2, p) + uv(V1, p) * uv(V2, p) / (Math.Sqrt(Math.Pow(lv(V1, p), 2) + Math.Pow(uv(V1, p), 2)) *
                       Math.Sqrt(Math.Pow(lv(V2, p), 2) + Math.Pow(uv(V2, p), 2))));
        }
        public static double GetCosineSimilarity(WeightVector V1, WeightVector V2)
        {
            return (V1.rating.Value * V2.rating.Value + V1.star.Value * V2.star.Value) / (Math.Sqrt(Math.Pow(V1.rating.Value, 2) + Math.Pow(V1.star.Value, 2)) *
                       Math.Sqrt(Math.Pow(V2.rating.Value, 2) + Math.Pow(V2.star.Value, 2)));
        }
    }
}

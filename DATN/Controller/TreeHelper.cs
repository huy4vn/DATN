using DATN.Model;
using RTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Controller
{
    class TreeHelper
    {
        RTree<DataPoint> tree;
        bool MaxPlayer = true;

        public TreeHelper(RTree<DataPoint> tree)
        {
            this.tree = tree;
        }
        public double getScore(DataPoint point, MBRModel<WeightVector> mv)
        {
            double totalScore = 0;

            totalScore =lv(mv, point);

            return totalScore;
        }
        public static double uv(MBRModel<WeightVector> mv, DataPoint p)
        {
            return (double)(mv.upperRight.rating * p.rating) + (double)(mv.upperRight.star * p.star);
        }
        public List<Node<DataPoint>> getChild(Node<DataPoint>node)
        {
            List<Node<DataPoint>> mayChild = new List<Node<DataPoint>>();
            List<Node<DataPoint>> result = new List<Node<DataPoint>>();
            foreach (var nodeItem in tree.nodeMap)
            {
                if (nodeItem.Value.level == node.level-1)
                {
                    mayChild.Add(nodeItem.Value);
                }
            }
            foreach(var mayChildNode in mayChild)
            {
                if (node.getMBR().contains(mayChildNode.getMBR()))
                {
                    result.Add(mayChildNode);
                }
            }
            return result;
        }
        public static double lv(MBRModel<WeightVector> mv, DataPoint p)
        {
            return (double)(mv.lowerLeft.rating * p.rating) + (double)(mv.lowerLeft.star * p.star);
        }
        public double IteratePoint(List<Rectangle> points,bool isMin, MBRModel<WeightVector> mv)
        {
            List<double> pointsScore = new List<double>();
            foreach(var item in points)
            {
                if (item != null)
                {

                    pointsScore.Add(getScore(new DataPoint(item), mv));
                }
            }
            if (isMin)
            {
                return pointsScore.Min();
            }
            else
            {
                return pointsScore.Max();
            }
        }
        public double Iterate(Node<DataPoint> node, int depth, double alpha, double beta, bool isMin, MBRModel<WeightVector> mv)
        {
            //for leaf
            if (node.isLeaf())
            {   
                return IteratePoint(node.entries.ToList(),!isMin,mv);
            }
            //for node
            if (isMin == false)
            {
                foreach (Node<DataPoint> child in getChild(node))
                {
                    alpha = Math.Max(alpha==Int32.MaxValue?Int32.MinValue:alpha, Iterate(child, depth - 1, alpha, beta, !isMin, mv));
                    if (beta < alpha)
                    {
                        break;
                    }

                }
                          return alpha;
            }
            else
            {
                foreach (Node<DataPoint> child in getChild(node))
                {
                    beta = Math.Min(alpha == Int32.MinValue ? Int32.MaxValue : alpha, Iterate(child, depth - 1, alpha, beta, !isMin, mv));

                    if (beta < alpha)
                    {
                        break;
                    }
                }

                return beta;
            }
        }
    }
}

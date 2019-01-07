using DATN.Model;
using RTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Controller
{
    class INTOPKController
    {
        RTree.RTree<DataPoint> tree;
        public List<DataPoint> listItem = new List<DataPoint>();
        TreeHelper treeHelper;
        String fileName="DataPoint.data";
        public void getTree()
        {
            
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\"));
            //not get tree before
            if (tree == null)
            {
                //get from directory
                RTree.RTree<DataPoint> tree = SaveDataController.ReadFromBinaryFile<RTree.RTree<DataPoint>>(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\", fileName));
                //tree not created before
                tree.locker = new System.Threading.ReaderWriterLock();
                if (tree.Count == 0 || (tree.Count > 0 && tree.Count != listItem.Count))
                {
                    //create tree
                    tree = new RTree.RTree<DataPoint>(listItem.Count / 2, 2);
                    int count = 0;
                    foreach (DataPoint p in listItem)
                    {
                        count++;
                        Debug.WriteLine("INTOP - " + count);
                        RTree.Rectangle rect = new RTree.Rectangle((float)p.rating, (float)p.star, (float)p.rating, (float)p.star, 0, 0);
                        tree.Add(rect, p);
                    }
                    //save tree to file
                    SaveDataController.WriteToBinaryFile(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\", fileName), tree);
                }

                this.tree = tree;
                treeHelper = new TreeHelper(tree);
            }
        }
        public MBRModel<DataPoint> getRoot()
        {
            //getTree();
            //get root after add to tree 
            RTree.Rectangle bounds = tree.getBounds();
            DataPoint upperRight = new DataPoint(bounds.get(0).GetValueOrDefault().max, bounds.get(1).GetValueOrDefault().max);
            DataPoint lowerLeft = new DataPoint(bounds.get(0).GetValueOrDefault().min, bounds.get(1).GetValueOrDefault().min);
            //retrun root
            return new MBRModel<DataPoint>(lowerLeft, upperRight,tree.getNode(tree.getRootNodeId()));
        }
        public List<MBRModel<DataPoint>> getChildBoundsAndPoints(MBRModel<DataPoint> entries)
        {
            HashSet<MBRModel<DataPoint>> result = new HashSet<MBRModel<DataPoint>>();
            MBRModel<DataPoint> element = new MBRModel<DataPoint>();
            Dictionary<int, Node<DataPoint>> node;
            Dictionary<int, Node<DataPoint>> treeNode= tree.nodeMap;
            //entries is root
            if (tree.getBounds().Equals(new Rectangle((float)entries.lowerLeft.rating, (float)entries.lowerLeft.star, (float)entries.upperRight.rating, (float)entries.upperRight.star, 0, 0))){
                node = treeNode;
            }
            //not root
            else
            {
                if (!isDataPoint(entries))
                {
                    node = new Dictionary<int, Node<DataPoint>>();
                    foreach (var nodeItem in treeNode)
                    {
                        if (nodeItem.Value.mbr.Equals(new Rectangle((float)entries.lowerLeft.rating, (float)entries.lowerLeft.star, (float)entries.upperRight.rating, (float)entries.upperRight.star, 0, 0)))
                        {
                            node.Add(0,nodeItem.Value);
                            break;
                        }
                }
                }
                else
                {
                    result.Add(entries);
                    return result.ToList();
                }    
            }
            
            foreach(var item in node)
            {
                //add MBR
                if (!item.Value.mbr.Equals(new Rectangle((float)entries.lowerLeft.rating, (float)entries.lowerLeft.star, (float)entries.upperRight.rating, (float)entries.upperRight.star,0,0))) {
                result.Add(getPoint(item.Value.mbr, true,item.Value));
                }
            }
            if (result.Count == 0)
            {
                foreach (var item in node)
                {

                    //add dataPoint
                    foreach (var child in item.Value.entries)
                    {
                        if (child != null)
                        {
                            element = getPoint(child, false,null);
                            if (element != null)
                            {
                                result.Add(element);
                            }

                        }

                    }

                }
            }
            
            return result.ToList();
        }
        public MBRModel<DataPoint> getPoint(Rectangle rectangle,bool isRetangle,Node<DataPoint> node)
        {   
            MBRModel< DataPoint > result= new MBRModel<DataPoint>(new DataPoint(rectangle.min[0], rectangle.min[1]), new DataPoint(rectangle.max[0], rectangle.max[1]),node);
            if (!isRetangle)
            {
                if (isDataPoint(result))
                {
                    return result;
                }
                return null;
            }
            else
            {
                return result;
            }
            
        }
        public int IntopK(MBRModel<DataPoint> entries,MBRModel<WeightVector> mv, DataPoint q, int k)
        {
            //init value
            
            int precincPoints = 0, precEntries = 0;
            
            List<MBRModel<DataPoint>> C = new List<MBRModel<DataPoint>>();
            Queue<MBRModel<DataPoint>> HeapS = new Queue<MBRModel<DataPoint>>();
             if (uv(mv, q) > lv(mv, entries.lowerLeft))
            {
                HeapS.Enqueue(entries);
                if (uv(mv, entries.upperRight) < lv(mv, q))
                {
                    precEntries++;
                }
            }
             
            //while heap not empty
            while (HeapS.Count > 0)
            {
                entries = HeapS.Dequeue();
                if (precincPoints >= k && lv(mv, entries.lowerLeft) >= lv(mv, q))
                {
                    //if mv is single w
                    if (isDataPoint(mv))
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                if (uv(mv, entries.upperRight) < lv(mv, q))
                {
                    precEntries--;
                }
                //double test = treeHelper.Iterate(entries.node, entries.node.getLevel(), Int32.MinValue, Int32.MaxValue, entries.node.getLevel() % 2 != 0, mv);
                //double uscore = uv(mv, q);
                //if (test < uscore)
                //{
                C = expand(entries);
                //}
                foreach (var ei in C)
                {
                    if (uv(mv, q) > lv(mv, ei.lowerLeft))
                    {
                        if (uv(mv, ei.upperRight) < lv(mv, q))
                        {
                            precEntries++;
                            if (precEntries >= k)
                            {
                                return -1;
                            }
                        }
                        if (isDataPoint(ei))
                        {
                            precincPoints++;
                        }
                        else
                        {
                            HeapS.Enqueue(ei);
                        }
                    }
                }
               
            }
            
            if (precincPoints >= k)
            {
                return 0;
            }
            else
            {
                
                return 1;
            }
        }
        public bool isDataPoint(MBRModel<DataPoint> point)
        {
            return point.lowerLeft.rating == point.upperRight.rating && point.lowerLeft.star == point.upperRight.star;
        }
        public bool isDataPoint(MBRModel<WeightVector> point)
        {
            return point.lowerLeft.rating == point.upperRight.rating && point.lowerLeft.star == point.upperRight.star;
        }
        private List<MBRModel<DataPoint>> expand(MBRModel<DataPoint> entries)
        {
            List<MBRModel<DataPoint>> result = new List<MBRModel<DataPoint>>();
           
            result.AddRange(getChildBoundsAndPoints(entries));
            return result;
        }

        public static double uv(MBRModel<WeightVector> mv, DataPoint p)
        {
            return (double)(mv.upperRight.rating * p.rating) + (double)(mv.upperRight.star * p.star);
        }
        public static double lv(MBRModel<WeightVector> mv, DataPoint p)
        {
            return (double)(mv.lowerLeft.rating * p.rating) + (double)(mv.lowerLeft.star * p.star);
        }
    }
}

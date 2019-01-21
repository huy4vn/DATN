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
    class INTOPKController: BasicFunction
    {
        RTree.RTree<DataPoint> tree;
        public List<DataPoint> listItem = new List<DataPoint>();
    
        int currentHeight = 0;
        //TreeHelper treeHelper;
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
                var watch = System.Diagnostics.Stopwatch.StartNew();
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
                    watch.Stop();
                    Debug.WriteLine("INTOP - process Tree Time:" + watch.ElapsedMilliseconds);
                    //save tree to file
                    SaveDataController.WriteToBinaryFile(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\", fileName), tree);
                }

                this.tree = tree;
                //treeHelper = new TreeHelper(tree);
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
            return new MBRModel<DataPoint>(lowerLeft, upperRight);
        }
        public bool contains(MBRModel<DataPoint> e, Rectangle MBR)
        {
            return e.lowerLeft.star >= MBR.get(1).GetValueOrDefault().min && e.upperRight.star <= MBR.get(1).GetValueOrDefault().max && e.lowerLeft.rating >= MBR.get(0).GetValueOrDefault().min && e.upperRight.rating <= MBR.get(0).GetValueOrDefault().max;
        }
        public List<MBRModel<DataPoint>> getChildBoundsAndPoints(MBRModel<DataPoint> e)
        {
            HashSet<MBRModel<DataPoint>> result = new HashSet<MBRModel<DataPoint>>();

            Node<DataPoint> node;
            Dictionary<int, Node<DataPoint>> treeNode = tree.nodeMap;
            //List<WeightVector> node = tree.Contains(new Rectangle(e.lowerLeft.rating, e.lowerLeft.star, e.upperRight.rating, e.upperRight.star,0,0));
            List<DataPoint> list = tree.Contains(tree.getBounds());
            //entries is root
            if (tree.getBounds().Equals(new Rectangle((float)e.lowerLeft.rating, (float)e.lowerLeft.star, (float)e.upperRight.rating, (float)e.upperRight.star, 0, 0)))
            {
                node = tree.getNode(tree.getRootNodeId());
                currentHeight = tree.treeHeight;
            }
            //not root
            else
            {

                if (!isDataPoint(e))
                {
                    node = null;
                    //node = new Dictionary<int, Node<WeightVector>>();
                    foreach (var nodeItem in treeNode)
                    {
                        //same height
                        if (nodeItem.Value.level == e.height - 1)
                        {
                            if (contains(e, nodeItem.Value.mbr))
                            {
                                node = nodeItem.Value;
                                break;
                            }

                        }
                    }
                }
                else
                {
                    result.Add(e);
                    return result.ToList();
                }
            }

            foreach (var item in node.entries)
            {
                if (item != null)
                {
                    DataPoint upperRight = new DataPoint(item.get(0).GetValueOrDefault().max, item.get(1).GetValueOrDefault().max);
                    DataPoint lowerLeft = new DataPoint(item.get(0).GetValueOrDefault().min, item.get(1).GetValueOrDefault().min);
                    result.Add(new MBRModel<DataPoint>(lowerLeft, upperRight, node.level));
                }
            }
            return result.ToList();
        }
        public MBRModel<DataPoint> getPoint(Rectangle rectangle,bool isRetangle)
        {   
            MBRModel< DataPoint > result= new MBRModel<DataPoint>(new DataPoint(rectangle.min[0], rectangle.min[1]), new DataPoint(rectangle.max[0], rectangle.max[1]));
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
                HeapS = new Queue<MBRModel<DataPoint>>(HeapS.OrderBy(s => lv(mv, s.lowerLeft)));
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
        private List<MBRModel<DataPoint>> expand(MBRModel<DataPoint> entries)
        {
            List<MBRModel<DataPoint>> result = new List<MBRModel<DataPoint>>();
           
            result.AddRange(getChildBoundsAndPoints(entries));
            return result;
        }  
    }
}

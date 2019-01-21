﻿using DATN.Model;
using RTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DATN.Controller
{
    class BBRController : BasicFunction
    {

        INTOPKController intopkController;
        public List<WeightVector> listItem = new List<WeightVector>();
        int currentHeight = 0;

        public void setIntopController(INTOPKController iNTOPKController)
        {
            this.intopkController = iNTOPKController;
        }

        RTree.RTree<WeightVector> tree;
        String fileName = "WeightVector.data";
        public bool contains(WeightVector e, MBRModel<WeightVector> MBR)
        {
            return e.star >= MBR.lowerLeft.star && e.star <= MBR.upperRight.star && e.rating >= MBR.lowerLeft.rating && e.rating <= MBR.upperRight.rating;
        }

        public bool contains(MBRModel<WeightVector> e, Rectangle MBR)
        {
            return e.lowerLeft.star >= MBR.get(1).GetValueOrDefault().min && e.upperRight.star <= MBR.get(1).GetValueOrDefault().max && e.lowerLeft.rating >= MBR.get(0).GetValueOrDefault().min && e.upperRight.rating <= MBR.get(0).GetValueOrDefault().max;
        }

        public bool contains(MBRModel<WeightVector> e, MBRModel<WeightVector> MBR)
        {
            return e.lowerLeft.star >= MBR.lowerLeft.star && e.upperRight.star <= MBR.upperRight.star && e.lowerLeft.rating >= MBR.lowerLeft.rating && e.upperRight.rating <= MBR.upperRight.rating;
        }

        public void getTree()
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\"));
            if (tree == null)
            {
                RTree.RTree<WeightVector> tree = SaveDataController.ReadFromBinaryFile<RTree.RTree<WeightVector>>(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\", fileName));
                //not created before
                tree.locker = new System.Threading.ReaderWriterLock();
                if (tree.Count == 0 || (tree.Count>0 && tree.Count!=listItem.Count))
                {

                    tree = new RTree.RTree<WeightVector>(listItem.Count>9? 9:listItem.Count/2, 2);
                    int count = 0;
                    foreach (WeightVector p in listItem)
                    {
                        count++;
                        Debug.WriteLine(count);
                        RTree.Rectangle rect = new RTree.Rectangle((float)p.rating, (float)p.star, (float)p.rating, (float)p.star, 0, 0);
                        tree.Add(rect, p);
                    }

                    SaveDataController.WriteToBinaryFile(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\", fileName), tree);
                }
                this.tree = tree;

            }
        }

        public MBRModel<WeightVector> getRoot()
        {
            //getTree();
            //get root after add to tree 
            RTree.Rectangle bounds = tree.getBounds();
            WeightVector upperRight = new WeightVector(bounds.get(0).GetValueOrDefault().max, bounds.get(1).GetValueOrDefault().max);
            WeightVector lowerLeft = new WeightVector(bounds.get(0).GetValueOrDefault().min, bounds.get(1).GetValueOrDefault().min);
            //retrun root
            return new MBRModel<WeightVector>(lowerLeft, upperRight);
        }

        private HashSet<MBRModel<WeightVector>> expand(MBRModel<WeightVector> e)
        {
            HashSet<MBRModel<WeightVector>> result = new HashSet<MBRModel<WeightVector>>();
            foreach(var item in getChildBoundsAndPoints(e))
            {
                result.Add(item);
            }
            return result;
        }

        private List<WeightVector> expandAll(MBRModel<WeightVector> e)
        {
            return tree.Contains(new Rectangle((float)e.lowerLeft.rating, (float)e.lowerLeft.star, (float)e.upperRight.rating, (float)e.upperRight.star, 0, 0));
        }

        public List<MBRModel<WeightVector>> getChildBoundsAndPoints(MBRModel<WeightVector> e)
        {
            HashSet<MBRModel<WeightVector>> result = new HashSet<MBRModel<WeightVector>>();
      
            Node<WeightVector> node;
            Dictionary<int, Node<WeightVector>> treeNode= tree.nodeMap;
            //List<WeightVector> node = tree.Contains(new Rectangle(e.lowerLeft.rating, e.lowerLeft.star, e.upperRight.rating, e.upperRight.star,0,0));
            List<WeightVector> list = tree.Contains(tree.getBounds());
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
                        if (nodeItem.Value.level == e.height-1 )
                        {
                            if(contains(e, nodeItem.Value.mbr)) {
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
                    WeightVector upperRight = new WeightVector(item.get(0).GetValueOrDefault().max, item.get(1).GetValueOrDefault().max);
                    WeightVector lowerLeft = new WeightVector(item.get(0).GetValueOrDefault().min, item.get(1).GetValueOrDefault().min);
                    result.Add(new MBRModel<WeightVector>(lowerLeft, upperRight, node.level));
                }
               
            }
            return result.ToList();
        }

        public MBRModel<WeightVector> getPoint(Rectangle rectangle, bool isRetangle)
        {
            MBRModel<WeightVector> result = new MBRModel<WeightVector>(new WeightVector(rectangle.min[0], rectangle.min[1]), new WeightVector(rectangle.max[0], rectangle.max[1]));
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

        public KeyValuePair<int, HashSet<WeightVector>> BBR(DataPoint data,int rank)
        {

            MBRModel<WeightVector> mbr = getRoot();
            MBRModel<DataPoint> entries = intopkController.getRoot();
            Queue<MBRModel<WeightVector>> heapW = new Queue<MBRModel<WeightVector>>();
            HashSet<WeightVector> result = new HashSet<WeightVector>();
            heapW.Enqueue(mbr);
            int i,count=0;

            while (heapW.Count > 0)
            {
                MBRModel<WeightVector> e = heapW.Dequeue();

                i = intopkController.IntopK(entries,e, data,rank);
               
                count++;
                
                if (i == 0)
                {
                    
                    foreach(var item in expand(e))
                    {
                        heapW.Enqueue(item);
                        
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        foreach (var item in expandAll(e))
                        {
                            result.Add(item);
                        }

                    }
                }
                if (heapW.Count > 0 )
                {
                    List<MBRModel<WeightVector>> listSorted = SortList(heapW.ToList(), data);
                    heapW = new Queue<MBRModel<WeightVector>>();
                    foreach (var item in listSorted)
                    {
                        heapW.Enqueue(item);

                    }
                }
            }

            return new KeyValuePair<int, HashSet<WeightVector>>(count,result);
        }

        internal List<MBRModel<WeightVector>> SortList(List<MBRModel<WeightVector>> listW,DataPoint p)
        {
            List<MBRModel<WeightVector>> listResult = new List<MBRModel<WeightVector>>();

            List<MBRModel<WeightVector>> listWTemp = new List<MBRModel<WeightVector>>();
            listWTemp.AddRange(listW);
            MBRModel<WeightVector> similarW = new MBRModel<WeightVector>(new WeightVector(0.5,0.5), new WeightVector(0.5, 0.5));
            int totalW = listWTemp.Count;
            while (listResult.Count < totalW)
            {
                double max = Int32.MinValue;
                int index = -1;
                for (var i = 0; i < listWTemp.Count; i++)
                {
                    MBRModel<WeightVector> itemW = listWTemp[i];
                    double similarValue = GetCosineSimilarity(itemW, similarW,p);
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
        
    }
}

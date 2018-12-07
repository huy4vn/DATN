using DATN.Model;
using RTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DATN.Controller
{
    class BBRController
    {

        INTOPKController intopkController = new INTOPKController();
        RTree.RTree<WeightVector> tree;
        String fileName = "WeightVector.data";
        public bool contains(WeightVector e, MBRModel<WeightVector> MBR)
        {
            return e.star >= MBR.lowerLeft.star && e.star <= MBR.upperRight.star && e.rating >= MBR.lowerLeft.rating && e.rating <= MBR.upperRight.rating;
        }
        public bool contains(MBRModel<WeightVector> e, MBRModel<WeightVector> MBR)
        {
            return e.lowerLeft.star >= MBR.lowerLeft.star && e.upperRight.star <= MBR.upperRight.star && e.lowerLeft.rating >= MBR.lowerLeft.rating && e.upperRight.rating <= MBR.upperRight.rating;
        }
        public void getTree()
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\"));
            RTree.RTree<WeightVector> tree = SaveDataController.ReadFromBinaryFile<RTree.RTree<WeightVector>>(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\", fileName));
            //not created before
            tree.locker = new System.Threading.ReaderWriterLock();
            if (tree.Count == 0)
            {
                //query database
                WeightVectorEntities entities = new WeightVectorEntities();
                var query = from p in entities.WeightVectors
                            select p;
                List<WeightVector> listItem = query.ToList();
                tree = new RTree.RTree<WeightVector>(6, 2);
                foreach (WeightVector p in listItem)
                {
                    RTree.Rectangle rect = new RTree.Rectangle((float)p.rating, (float)p.star, (float)p.rating, (float)p.star, 0, 0);
                    tree.Add(rect, p);
                }
            }
            SaveDataController.WriteToBinaryFile(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\", fileName), tree);
            this.tree = tree;
        }
        public MBRModel<WeightVector> getRoot()
        {
            getTree();
            //get root after add to tree 
            RTree.Rectangle bounds = tree.getBounds();
            WeightVector upperRight = new WeightVector(bounds.get(0).GetValueOrDefault().max, bounds.get(1).GetValueOrDefault().max);
            WeightVector lowerLeft = new WeightVector(bounds.get(0).GetValueOrDefault().min, bounds.get(1).GetValueOrDefault().min);
            //retrun root
            return new MBRModel<WeightVector>(lowerLeft, upperRight);
        }
        private List<MBRModel<WeightVector>> expand(MBRModel<WeightVector> e)
        {
            List<MBRModel<WeightVector>> result = new List<MBRModel<WeightVector>>();

            result.AddRange(getChildBoundsAndPoints(e));
            return result;
        }
        private List<WeightVector> expandAll(MBRModel<WeightVector> e)
        {
            return tree.Contains(new Rectangle((float)e.lowerLeft.rating, (float)e.lowerLeft.star, (float)e.upperRight.rating, (float)e.upperRight.star, 0, 0));
        }
        public List<MBRModel<WeightVector>> getChildBoundsAndPoints(MBRModel<WeightVector> e)
        {
            HashSet<MBRModel<WeightVector>> result = new HashSet<MBRModel<WeightVector>>();
            MBRModel<WeightVector> element = new MBRModel<WeightVector>();
            Dictionary<int, Node<WeightVector>> node = tree.nodeMap;
            //List<WeightVector> node = tree.Contains(new Rectangle((float)e.lowerLeft.rating, (float)e.lowerLeft.star, (float)e.upperRight.rating, (float)e.upperRight.star,0,0));
        
            foreach (var item in node)
            {
                //add MBR
                if (!item.Value.mbr.Equals(tree.getBounds()) )
                {
                    MBRModel<WeightVector> point = getPoint(item.Value.mbr, true);
                    if (contains(point, e))
                    {
                        result.Add(point);
                    }
                }
            }
            foreach (var item in node)
            {

                //add dataPoint
                foreach (var child in item.Value.entries)
                {
                    if (child != null)
                    {
                        element = getPoint(child, false);
                        if (element != null)
                        {
                            result.Add(element);
                        }

                    }

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
        public bool isDataPoint(MBRModel<WeightVector> point)
        {
            return point.lowerLeft.rating == point.upperRight.rating && point.lowerLeft.star == point.upperRight.star;
        }
        public List<WeightVector> BBR(DataPoint data,int rank)
        {
            MBRModel<WeightVector> mbr = getRoot();
            Queue<MBRModel<WeightVector>> heapW = new Queue<MBRModel<WeightVector>>();
            List<WeightVector> result = new List<WeightVector>();
            heapW.Enqueue(mbr);
            int i;
            while (heapW.Count > 0)
            {
                MBRModel<WeightVector> e = heapW.Dequeue();
                //TODO wating for Q to implement INTOPK, right now will create random result
                i = intopkController.IntopK(e, data,rank);
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
                        result.AddRange(expandAll(e));
                    }
                }
            }
            return result;
        }
    }
}

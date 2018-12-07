using RTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Model
{
    class MBRModel<T>
    {
        public MBRModel()
        {
        }

        public MBRModel(T lowerLeft, T upperRight)
        {
            this.lowerLeft = lowerLeft;
            this.upperRight = upperRight;
        }
        public MBRModel(T lowerLeft, T upperRight,Node<T> node)
        {
            this.lowerLeft = lowerLeft;
            this.upperRight = upperRight;
            if (node != null)
            {
            
                this.node = node;
            }
        }
        public Node<T> node { get; set; }
        public T lowerLeft { get; set; }
        public T upperRight { get; set; }
    }
}

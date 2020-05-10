using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoffman.Data
{

    public class Node : IComparable<Node>
    {
        public int i { get; set; }
        public char c { get; set; }
       
        public Node Left { get; set; }
        public Node Right { get; set; }

        public int CompareTo(Node node)
        {
            if (node == null)
                return 1;
            else
                return this.i.CompareTo(node.i);
        }
    }
}

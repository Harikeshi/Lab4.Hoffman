using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hoffman.Data
{
    public static class ExtensionMethods
    {
        public static LinkedList<Node> QSort(this LinkedList<Node> lst)
        {

            List<Node> l = new List<Node>(lst.Count);

            int length = lst.Count;
            for (int i = 0; i < length; i++)
            {
                l.Add(lst.First.Value);
                lst.RemoveFirst();
            }
            l.Sort();
            for (int i = 0; i < l.Count; i++)
            {
                lst.AddLast(l[i]);
            }
            return lst;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Path_Node<T>
    {
        public T data;
        public Path_Edge<T>[] edges;
    }
}

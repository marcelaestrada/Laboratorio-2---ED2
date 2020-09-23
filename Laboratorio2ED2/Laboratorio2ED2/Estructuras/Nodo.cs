using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio2ED2
{
    public class Nodo<T> where T:IComparable
    {
        public T[] values { get; set; }
        public int[] posicionI { get; set; }
        public int[] posicionD { get; set; }
        public int contador { get; set; }
    }
}

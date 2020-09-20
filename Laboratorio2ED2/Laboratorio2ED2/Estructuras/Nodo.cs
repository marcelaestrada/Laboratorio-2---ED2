using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio2ED2
{
    public class Nodo<T> where T:IComparable
    {
        public T[] values { get; set; }
        public Nodo<T> hijoI { get; set; }
        public Nodo<T> hijoD { get; set; }
        public Nodo<T> padre { get; set; }
        public int contador { get; set; }

    }
}

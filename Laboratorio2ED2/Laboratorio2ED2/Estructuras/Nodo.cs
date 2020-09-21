using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Laboratorio2ED2
{
    internal class Nodo<T> where T: IComparable
    {
        internal T[] Values { get; set; }

        internal int Id { get; set; }

        internal int Padre { get; set; }

        internal int[] Hijos { get; set; }


    }
}

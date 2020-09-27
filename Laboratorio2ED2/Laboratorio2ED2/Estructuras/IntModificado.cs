using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Laboratorio2ED2.Estructuras
{
    class IntModificado : IComparable
    {
        public int Number { get; set; }
        public IntModificado()
        {

        }

        public int CompareTo(object obj)
        {
            return Number.CompareTo(((IntModificado)obj).Number);
        }
    }
}

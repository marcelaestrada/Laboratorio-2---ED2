using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Laboratorio2ED2
{
    public class ArbolB<T> : iArbol<T> where T : IComparable
    {
        int min;
        int max;
        int mitad;

        public void Insertar(T value, int grado, int idCorrespondiente)
        {
            definirValores(grado);
        }

        public void insertarNodo(T value)
        {

        }

        public void Eliminar(T value)
        {
            throw new NotImplementedException();
        }

        public T Buscar(T value)
        {
            throw new NotImplementedException();
        }

        void definirValores (int grado)
        {
            //valores minimos
            min = (grado - 1) / 2;

            //valores maximos
            max = grado - 1;

            //numero a subir
            if ((grado % 2) == 0)
            {
                mitad = grado / 2;
            }
            else
            {
                mitad = (grado + 1) / 2;
            }
        }
    }
}

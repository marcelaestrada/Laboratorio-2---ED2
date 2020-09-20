using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Laboratorio2ED2
{
    public class ArbolB<T> : iArbol<T> where T : IComparable
    {
        public Nodo<T> root = new Nodo<T>();
        public void Insertar(T value, int grado)
        {
            int maxValues = maxValores(grado);
            int minValues = minValores(grado);
            int mitadValues = mitadValores(grado);
        }
        
        void insertarValue(Nodo<T> root, int max, int min, int mitad)
        {
            
        }

        int maxValores(int grado)
        {
            int maxValues = grado - 1;
            return maxValues;
        }
        int minValores(int grado)
        {
            int minValues;
            double min = (grado / 2) - 1;
            if ((min % 1) != 0){
                minValues = (int)min;
            }
            else
            {
                minValues = Convert.ToInt32(min);
            }
            return minValues;
        }
        int mitadValores(int grado)
        {
            int mitadValues;
            double mitad = grado / 2;
            if ((mitad % 2) != 0)
            {
                mitadValues = (int)mitad;
            }
            else
            {
                mitadValues = Convert.ToInt32(mitad);
            }
            return mitadValues;
        }
        public Nodo<T> division(Nodo<T> valores, int mitad, int max)
        {
            Nodo<T> nuevoNodoI = new Nodo<T>();
            Nodo<T> nuevoNodoD = new Nodo<T>();

            for (int i = 0; i < mitad; i++)
            {
                nuevoNodoI.values[i] = valores.values[i];
            }
            for (int i = mitad + 1; i < max; i++)
            {
                int j = 0;
                nuevoNodoD.values[j] = valores.values[i];
                j++;
            }

            if (valores.padre == null)
            {
                valores.padre.values[0] = valores.values[mitad];
                valores.padre.hijoI = nuevoNodoI;
                valores.padre.hijoD = nuevoNodoD;
            }
            else
            {
                if (valores.padre.contador < max)
                {
                    for(int i = 0; i < valores.padre.contador; i++)
                    {
                        if (valores.padre.values[i]==null)
                        {
                            valores.padre.values[i] = valores.values[mitad];
                        }
                    }
                    for(int i =0; i < valores.padre.contador; i++)
                    {
                        if (valores.padre.values[i].CompareTo(valores.padre.values[i + 1]) == 1)
                        {
                            T aux = valores.padre.values[i];
                            valores.padre.values[i] = valores.padre.values[i + 1];
                            valores.padre.values[i + 1] = aux;
                        }
                    }
                    valores.padre.hijoI = nuevoNodoI;
                    valores.padre.hijoD = nuevoNodoD;
                }
                else
                {
                    division(valores.padre, mitad, max);
                }
            }
            return valores;
        }
        public void Eliminar(T value)
        {
            throw new NotImplementedException();
        }

        public T Buscar(T value)
        {
            throw new NotImplementedException();
        }




    }
}

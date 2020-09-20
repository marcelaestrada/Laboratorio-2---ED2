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

            insertarValue(root, maxValues, minValues, mitadValues, value);
        }
        void insertarValue(Nodo<T> nodo, int max, int min, int mitad, T valor)
        {
            for(int i = 0; i < max; i++)
            {
                if (nodo.values[i].CompareTo(valor) == 1)
                {
                    if (nodo.hijoI != null)
                    {
                        insertarValue(nodo.hijoI, max, min, mitad, valor);
                    }
                    else
                    {
                        insertarOrdenar(nodo, valor);
                        if (nodo.contador > max)
                        {
                            division(nodo, mitad, max);
                        }
                    }
                }
                else
                {
                    if (nodo.hijoD != null)
                    {
                        insertarValue(nodo.hijoD, max, min, mitad, valor);
                    }
                    else
                    {
                        insertarOrdenar(nodo, valor);
                        if (nodo.contador > max)
                        {
                            division(nodo, mitad, max);
                        }
                    }
                }
            }
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
        public void division(Nodo<T> valores, int mitad, int max)
        {
            Nodo<T> nuevoNodoD = new Nodo<T>();
            T valor = valores.values[mitad];

            for (int i = mitad + 1; i < max; i++)
            {
                int j = 0;
                nuevoNodoD.values[j] = valores.values[i];
                j++;
            }

            for (int i = mitad; i < max; i++)
            {
                valores.values[i] = default(T);
            }


            if (valores.padre == null)
            {
                valores.padre.values[0] = valor;
                valores.padre.hijoI = valores;
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
                            valores.padre.values[i] = valor;
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
                    valores.padre.hijoI = valores;
                    valores.padre.hijoD = nuevoNodoD;
                }
                else
                {
                    division(valores.padre, mitad, max);
                }
            }
        }
        public void insertarOrdenar(Nodo<T> node, T valor)
        {
            for (int i = 0; i < root.contador; i++)
            {
                if (root.values[i] == null)
                {
                    root.values[i] = valor;
                }
            }
            for (int i = 0; i < root.contador; i++)
            {
                if (root.values[i].CompareTo(root.values[i + 1]) == 1)
                {
                    T aux = root.values[i];
                    root.values[i] = root.values[i + 1];
                    root.values[i + 1] = aux;
                }
            }
            node.contador++;
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

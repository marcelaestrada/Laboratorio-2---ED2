using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Laboratorio2ED2
{
    public class ArbolB<T> : iArbol<T> where T : IComparable
    {
        int min;
        int max;
        int mitad;
        FileStream path = File.Create(@"c:\ArchivoPeliculas.txt");


        public void Insertar(T value, int grado, int idCorrespondiente)
        {
            definirValores(grado);
            insertarEnNodo(value, idCorrespondiente, grado);
        }

        public void insertarEnNodo(T value, int id, int grado)
        {
            if (id != 1)
            {
                //si hay nodo, hay que hacer comprobaciones 
                
            }
            else
            {
                //no hay ningun nodo creado
                Nodo<T> nuevoNodo = new Nodo<T>(max, grado);
                nuevoNodo.Id = id;
                nuevoNodo.Padre = -1;
                nuevoNodo.Values[0] = value;
                nuevoNodo.Order = nuevoNodo.GradoArbol = grado; //creo que seria lo mismo
                nuevoNodo.CountOfValues++;
                nuevoNodo.WriteToFile(path,1);
            }
        }

        public void convertirRuta()
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

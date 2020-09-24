using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Laboratorio2ED2
{
    public class ArbolB<T> : iArbol<T> where T : IComparable
    {
        int gradoArbol;
        int min;
        int max;
        int mitad;
        FileStream path = File.Create(@"c:\ArchivoPeliculas.txt");

        internal ArbolB(int grado)
        {
            gradoArbol = grado;
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

        public void Insertar(T value, int idCorrespondiente)
        {
            insertarEnNodo(value, idCorrespondiente);
        }

        public void insertarEnNodo(T value, int id)
        {
            if (id != 1)
            {
                //si hay nodo, hay que hacer comprobaciones 
                
            }
            else
            {
                //no hay ningun nodo creado
                Nodo<T> nuevoNodo = new Nodo<T>(max, gradoArbol);
                nuevoNodo.Id = id;
                nuevoNodo.Padre = -1;
                nuevoNodo.Values[0] = value;
                nuevoNodo.Order = gradoArbol; 
                nuevoNodo.CountOfValues++;
                nuevoNodo.WriteToFile(path,1);
            }
        }

        public void divisionNodo(String cambio, int id)
        {
            Nodo<T> newNodo = new Nodo<T>(max, gradoArbol);
            Nodo<T> newNodo2 = new Nodo<T>(max, gradoArbol);

            
        }

        public Nodo<T> convertirStringNodo(String aConvertir)
        {
            Nodo<T> recuperado = new Nodo<T>(max, gradoArbol);
            string[] datos = aConvertir.Split("|");
            String jsonString = File.ReadAllText(datos[2]);
            String jsonString2 = File.ReadAllText(datos[3]);            

            recuperado.Id = Convert.ToInt32(datos[0]);
            recuperado.Padre = Convert.ToInt32(datos[1]);

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

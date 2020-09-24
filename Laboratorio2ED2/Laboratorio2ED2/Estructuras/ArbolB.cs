﻿using Newtonsoft.Json;
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
        int encabezadoLength;
        string ruta;
        FileStream path;

        internal ArbolB(int grado, string ruta)
        {
            this.ruta = ruta;

            //Por motivos de depuración
            //this.ruta = @"c:\ArchivoPeliculas.txt";

            path = File.Create(this.ruta);
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
            string linea = LeerLineaArchivo(2, 1061, ruta);
            convertirStringNodo(linea);
        }

        public void insertarEnNodo(T value, int id)
        {
            if (id != 1)
            {
                //si hay nodo, recuperar raiz
                //evaluar con los valores si es mayor o menor
                //buscar hijo dependiendo del resultado anterior
                //evaluar otra vez , recursividad
                //al encontrar donde le corresponde, insertar y ver si hay que hacer división
                //llamar al metodo división o solo insertar y enviar de nuevo al archivo 


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

        private Nodo<T> convertirStringNodo(String aConvertir)
        {
            Nodo<T> recuperado = new Nodo<T>(max, gradoArbol);
            string[] datos = aConvertir.Split("|");
            string[] hijosPosiciones = datos[2].Split("/");
            String[] valuesDatos = datos[3].Split("/");            

            recuperado.Id = Convert.ToInt32(datos[0]);
            recuperado.Padre = Convert.ToInt32(datos[1]);
            for(int i = 0; i < hijosPosiciones.Length; i++)
            {
                recuperado.Hijos[i] = Convert.ToInt32(hijosPosiciones[i]);
            }
            for (int i = 0; i < valuesDatos.Length; i++)
            {
                recuperado.Values[i] = (T)JsonConvert.DeserializeObject(valuesDatos[i]);
            }
            return recuperado;
        }
        public void Eliminar(T value)
        {
            //Ir a encabezado del archivo y recuperar id de raiz. 
            int idRaiz = LeerRaizEncabezado();
            EliminarValor(idRaiz, value);
            throw new NotImplementedException();
        }

        private void EliminarValor(int id, T value)
        {
            //file.Write(Encoding.UTF8.GetBytes(ToFixedLengthString()), 0, FixedSizedText);

            Nodo<T> aux1 = new Nodo<T>(this.max, this.gradoArbol);
            Nodo<T> aux2 = new Nodo<T>(this.max, this.gradoArbol);

            int position = id;

            string data = LeerLineaArchivo(position, aux1.FixedSizedText, ruta);


            
            //Ir a posicion de raiz o nodo en la recursividad. ok
            //Hacer la lectura de archivo y asignar valores al nodo aux1.ok
            
            //Evaluear si el valor buscado es el mismo en el campo hijos. 
            //Si no, evaluear menor o mayor el x numero de veces
            //Usar el número de validaciones para ir a la posicion del arreglo hijos. 
            //Recuperar id del nodo hijo al que se movio. 

            //REPETIR RECURCIVIDAD. 

            //Al encontrar el valor evaluar casos de eliminacion. 

        }

        private string LeerLineaArchivo(int position, int fixedSizedText ,string ruta)
        {
            FileStream file = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            file.Seek( fixedSizedText * position + encabezadoLength, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(file);
            string respuesta = reader.ReadLine();
            file.Close();
            return respuesta;
        }

        public T Buscar(T value)
        {
            throw new NotImplementedException();
        }

        private int LeerRaizEncabezado()
        {
           throw new NotImplementedException();
        }
    }
}

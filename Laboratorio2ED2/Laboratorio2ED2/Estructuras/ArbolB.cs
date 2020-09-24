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

        private void EliminarValor(int idPosition, T value)
        {
            Nodo<T> nodoActual = new Nodo<T>(this.max, this.gradoArbol);

            string data = LeerLineaArchivo(idPosition, nodoActual.FixedSizedText, ruta);
            nodoActual = convertirStringNodo(data);

            int posicionHijos = 0;
            bool valorEncontrado = false;

            for (int i = 0; i <= gradoArbol; i++)
            {
                if (value.CompareTo(nodoActual.Values[i]) == 0)
                {
                    CasosEliminacion();
                    valorEncontrado = true;
                    break;
                }
                else if (value.CompareTo(nodoActual.Values[0]) == -1)//Es menor que la primera posición del arreglo de valores
                    break;
                else if (value.CompareTo(nodoActual.Values[i]) == 1)
                    posicionHijos++;
            }

            if (!valorEncontrado)
                EliminarValor(nodoActual.Hijos[posicionHijos], value);
        }

        private void CasosEliminacion()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Este método recupera el string completo del nodo dentro del archivo de texto. 
        /// </summary>
        /// <param name="position">Posición o id al que el cursosr debe moverse para leer en el archivo</param>
        /// <param name="fixedSizedText">Tamaño formateado predefinido en la clse nodo, respecto al string de cada uno</param>
        /// <param name="ruta">Ruta en la que se encuentra el archivo</param>
        /// <returns></returns>
        private string LeerLineaArchivo(int position, int fixedSizedText, string ruta)
        {
            FileStream file = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            file.Seek(fixedSizedText * position + encabezadoLength, System.IO.SeekOrigin.Begin);
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

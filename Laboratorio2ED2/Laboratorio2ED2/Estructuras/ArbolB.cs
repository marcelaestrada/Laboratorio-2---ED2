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
                mitad--;
            }
            else
            {
                mitad = (grado + 1) / 2;
                mitad--;
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
                Nodo<T> raiz = new Nodo<T>(max, gradoArbol);
                //si hay nodo, recuperar raiz
                String sRaiz = LeerLineaArchivo(1, raiz.FixedSizedText, ruta);
                raiz = convertirStringNodo(sRaiz);

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

        public int divisionEscritura(string cambio, int id)
        {
            Nodo<T> nodoCambiar = convertirStringNodo(cambio);
            Nodo<T> padre = new Nodo<T>(max, gradoArbol);
            Nodo<T> hijoD = new Nodo<T>(max, gradoArbol);
            Nodo<T> hijoDivisionDoble = new Nodo<T>(max, gradoArbol);
            int cantidadValores = nodoCambiar.Values.Length;

            if (id != 1)
            {
                //ya tiene padre
                //recuperar padre 
                string sRaiz = LeerLineaArchivo(nodoCambiar.Padre, nodoCambiar.FixedSizedText, ruta);
                padre = convertirStringNodo(sRaiz);

                if (padre.CountOfValues < max)
                {
                    //puede subirse el valor al padre 
                    agregarAPadre(padre, nodoCambiar);
                    definirDerecho(nodoCambiar, hijoD, id+1, padre.Id);
                    definirIzquierdo(nodoCambiar, id, padre.Id);
                    id++;
                    agregarHijos(padre, hijoD.Id);
                }
                else
                {
                    //se debe hacer dos divisiones, una del nodo y uno de la raiz

                }
            }
            else
            {
                //cuando aun no tiene padre, primera division
                //definicion de raiz
                definirRaiz(nodoCambiar, padre, id);
                id++;
                definirDerecho(nodoCambiar, hijoD, id+1, padre.Id);
                definirIzquierdo(nodoCambiar, id, padre.Id);
                id++;
                agregarHijos(padre, nodoCambiar.Id);
                agregarHijos(padre, hijoD.Id);
            }
            return id;
        }
        private void definirRaiz (Nodo<T> nodoCambiar, Nodo<T> newNodo, int id)
        {
            newNodo.Values[0] = nodoCambiar.Values[mitad];
            newNodo.Id = id;
            newNodo.Padre = -1;
            newNodo.CountOfValues++;
            newNodo.Order = gradoArbol;
        }
        private void definirDerecho (Nodo<T> nodoCambiar, Nodo<T> newNodo2, int id, int idPadre)
        {
            int cantidadValores = nodoCambiar.Values.Length;
            int contador = 0;
            int posicion = 0;
            for (int i = mitad + 1; i < nodoCambiar.Values.Length; i++)
            {
                newNodo2.Values[posicion] = nodoCambiar.Values[i];
                posicion++;
                contador++;
            }
            newNodo2.Id = id;
            newNodo2.Padre = idPadre;
            newNodo2.CountOfValues = cantidadValores - contador;
        }
        private void definirIzquierdo (Nodo<T> nodoCambiar, int id, int idPadre)
        {
            int cantidadValores = nodoCambiar.Values.Length;
            int contador2 = 0;
            for (int i = mitad; i < nodoCambiar.Values.Length; i++)
            {
                nodoCambiar.Values[i] = default(T);
                contador2++;
            }
            nodoCambiar.Id = id;
            nodoCambiar.Padre = idPadre;
            nodoCambiar.CountOfValues = cantidadValores - contador2;
        }
        private void agregarHijos(Nodo<T> nodo, int pos)
        {
            for(int i = 0; i<max; i++)
            {
                if (nodo.Hijos[i] == null)
                {
                    nodo.Hijos[i] = pos;
                    break;
                }
            }
        }
        private void agregarAPadre(Nodo<T> padre, Nodo<T> nodoCambiar)
        {
            int contador = 0;
            T aux;
            for(int i=0; i < max; i++)
            {
                if (contador == 0)
                {
                    //agregar valor a padre
                    if (padre.Values[i] == null)
                    {
                        padre.Values[i] = nodoCambiar.Values[mitad];
                    }
                    contador++;
                } 
            }

            //ordenar valores
            for(int i=0; i<max; i++)
            {
                if (padre.Values[i].CompareTo(padre.Values[i + 1])==1)
                {
                    aux = padre.Values[i];
                    padre.Values[i] = padre.Values[i + 1];
                    padre.Values[i + 1] = aux;

                }
            }
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

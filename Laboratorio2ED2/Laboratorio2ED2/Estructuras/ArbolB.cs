﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace Laboratorio2ED2
{
    public class ArbolB<T> : iArbol<T> where T : IComparable
    {
        private int gradoArbol;
        private int min;
        private int max;
        private int mitad;
        private int encabezadoLength;
        private string ruta;
        private FileStream path;

        internal ArbolB(int grado, string ruta)
        {
            this.ruta = ruta;

            //Por motivos de depuración
            //this.ruta = @"c:\ArchivoPeliculas.txt";

            path = File.Create(this.ruta);
            path.Close();
            
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
            string raiz = LeerLineaArchivo(1, 1061, ruta);
            insertarEnNodo(raiz, value, idCorrespondiente);
        }
        public void insertarEnNodo(string sRaiz, T value, int id)
        {
            if (id != 1)
            {
                Nodo<T> raiz = new Nodo<T>(max, gradoArbol);
                int contador = 0;

                //si hay nodo, recuperar raiz
                raiz = convertirStringNodo(sRaiz);

                //evaluar con los valores si es mayor o menor
                while (raiz.Hijos.Length != 0)
                {
                    for(int i = 0; i<raiz.Values.Length; i++)
                    {
                        if (raiz.Values[i].CompareTo(value) == 1)
                        {
                            contador = raiz.Hijos[i];
                            break;
                        }else if (raiz.Values[i].CompareTo(value) == -1)
                        {
                            contador = raiz.Hijos[i + 1];
                        }
                    }
                    raiz = convertirStringNodo(LeerLineaArchivo(contador, raiz.FixedSizedText, ruta));
                    insertarEnNodo(raiz.ToString(), value, id);
                }
                for (int i = 0; i < max; i++)
                {
                    if (raiz.Values[i] == null)
                    {
                        raiz.Values[i] = value;
                        break;
                    }
                }
                if(raiz.Values.Length == max)
                {
                    divisionEscritura(raiz.ToString(), id);
                }
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

            if (id > 1)
            {
                //ya tiene padre
                //recuperar padre 
                string sRaiz = LeerLineaArchivo(nodoCambiar.Padre, nodoCambiar.FixedSizedText, ruta);
                padre = convertirStringNodo(sRaiz);

                if (padre.CountOfValues < max-1)
                {
                    //puede subirse el valor al padre 
                    agregarAPadre(padre, nodoCambiar);
                    definirDerecho(nodoCambiar, hijoD, id+1, padre.Id);
                    definirIzquierdo(nodoCambiar, id, padre.Id);
                    id++;
                    agregarHijos(padre, hijoD.Id);
                    padre.WriteToFile(path, padre.Id);
                    nodoCambiar.WriteToFile(path, nodoCambiar.Id);
                    hijoD.WriteToFile(path, hijoD.Id);
                }
                else
                {
                    //division doble
                    if (padre.Padre == -1)
                    {
                        definirRaiz(padre, nodoCambiar, 1);
                        id++;
                        definirDerecho(nodoCambiar, hijoD, id + 1, padre.Id);
                        definirIzquierdo(nodoCambiar, id, padre.Id);
                        id++;
                        agregarHijos(padre, padre.Id);
                        agregarHijos(nodoCambiar, nodoCambiar.Id);
                        agregarHijos(hijoD, hijoD.Id);
                        padre.WriteToFile(path, padre.Id);
                        nodoCambiar.WriteToFile(path, nodoCambiar.Id);
                        hijoD.WriteToFile(path, hijoD.Id);
                    }
                    else
                    {
                        agregarAPadre(padre, nodoCambiar);
                        definirDerecho(padre, hijoD, id + 1, padre.Id);
                        definirIzquierdo(nodoCambiar, id, padre.Id);
                        id++;
                        agregarHijos(padre, hijoD.Id);
                        divisionEscritura(padre.ToString(), id);
                    }
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
                padre.WriteToFile(path, padre.Id);
                nodoCambiar.WriteToFile(path, nodoCambiar.Id);
                hijoD.WriteToFile(path, hijoD.Id);
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
            int j = 0;
            for (int i = mitad + 1; i < nodoCambiar.Values.Length; i++)
            {
                newNodo2.Values[posicion] = nodoCambiar.Values[i];
                posicion++;
                contador++;
            }
            for(int i = mitad+1; i == gradoArbol; i++)
            {
                newNodo2.Hijos[j] = nodoCambiar.Hijos[i];
                j++;
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
            for (int i = mitad; i < nodoCambiar.Hijos.Length; i++)
            {
                nodoCambiar.Hijos[i] = default(int);
            }
            nodoCambiar.Id = id;
            nodoCambiar.Padre = idPadre;
            nodoCambiar.CountOfValues = cantidadValores - contador2;
        }
        private void agregarHijos(Nodo<T> nodo, int pos)
        {
            for(int i = 0; i<max; i++)
            {
                if (nodo.Hijos[i] == 0)
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
            padre.CountOfValues++;
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
            
            EliminarValor(1, value);
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
                    CasosEliminacion(nodoActual, posicionHijos, i);
                   
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

        private void CasosEliminacion(Nodo<T> nodoActual, int posicionHijo, int posicionValor)
        {

            //1. Si sus dos hijos son vacios, eliminar valor y reordenar. 
            if (nodoActual.Hijos[posicionHijo] == 0 && nodoActual.Hijos[posicionHijo + 1] == 0)
            {
                if (nodoActual.CountOfValues-1 < this.min)
                {
                    //Raíz en común baja y sube mayor o menor a la raiz. 
                    nodoActual.Values[posicionValor] = default;
                    
                  

                    string data = LeerLineaArchivo(nodoActual.Padre, nodoActual.FixedSizedText, ruta);
                    Nodo<T> padreActual = convertirStringNodo(data);
                    T raizBaja = padreActual.Values[posicionHijo];
                    nodoActual.Values[posicionValor] = raizBaja;
                    //Subir al padre menor de la derecha. 
                    string dataHermano = LeerLineaArchivo(padreActual.Hijos[posicionHijo+1], nodoActual.FixedSizedText, ruta);
                    Nodo<T> hermanoDerecho = convertirStringNodo(dataHermano);
                    padreActual.Values[posicionHijo] = hermanoDerecho.Values[0];
                    hermanoDerecho.Values[0] = default;
                    Array.Sort(hermanoDerecho.Values);

                        //Si el que presta se queda en underFlow unir al nodo actual. 
                        //Bajar la raiz en común, unir el nodo hijo derecho con nodo actual. 
                        //Correr u ordenar valores del nodo padre. 
                    if (hermanoDerecho.CountOfValues < this.min)
                    {
                        raizBaja = padreActual.Values[posicionHijo];
                        padreActual.Values[posicionHijo] = default;
                        nodoActual.Values[posicionHijo + 1] = raizBaja;
                        //Agregar todos los valore del herman derecho al izquierdo. 
                        int contadorValoresDerecho = 0;
                        for (int i = posicionHijo+2; i < nodoActual.Values.Length; i++)
                        {
                            if (hermanoDerecho.Values == default)
                                break;

                            nodoActual.Values[i] = hermanoDerecho.Values[contadorValoresDerecho];
                            hermanoDerecho.Values[contadorValoresDerecho] = default;
                            contadorValoresDerecho++;
                        }
                        Array.Sort(padreActual.Values);
                        
                        //Juntar Hijos
                        padreActual.Hijos[padreActual.ConteoHijos() - 1] = 0;
                        int hijosDerecho = 0;
                        for (int i = nodoActual.ConteoHijos(); i < hermanoDerecho.Hijos.Length; i++)
                        {
                            try
                            {
                                nodoActual.Hijos[i] = nodoActual.Hijos[hijosDerecho];
                                hijosDerecho++;
                            }
                            catch (IndexOutOfRangeException a)
                            {

                                break;
                            }

                        }
                    }
                    //Sobre escribir los tres nodos;
                    FileStream file = new FileStream(this.ruta, FileMode.Open, FileAccess.Write);
                    nodoActual.WriteToFile(file, nodoActual.Id);
                    padreActual.WriteToFile(file, padreActual.Id);
                    hermanoDerecho.WriteToFile(file, hermanoDerecho.Id);


                }
                else
                {
                    nodoActual.Values[posicionValor] = default;
                    Array.Sort(nodoActual.Values);

                    FileStream file = new FileStream(this.ruta, FileMode.Open, FileAccess.Write);
                    nodoActual.WriteToFile(file, nodoActual.Id);
                    file.Close();
                }

            }
            //2. Si tiene algun hijo ir a traer el mayor de la izq o el menor de la der
            else if (nodoActual.Hijos[posicionHijo] != 0 || nodoActual.Hijos[posicionHijo + 1] != 0)
            {
                nodoActual.Values[posicionValor] = default;

                //Recuperar más a la derecha de la izquierda
                T newValue = default;

                //Si viene false es por que queda en underflow
                if (!DeIzquierdaADerecha(nodoActual.Hijos[posicionValor], ref newValue))
                {
                    if (nodoActual.Hijos[posicionHijo+1] != 0)
                    {
                        if (!DeDerechaAIzquierda(nodoActual.Hijos[posicionValor + 1], ref newValue))
                        {
                            //Si ambos quedan en underflow escojer un lado ir hasta ese ultimo y obtener valor de alli. 
                            //Luego rebalancear alli 
                            nodoActual.Values[posicionValor] = DeIzquierdaADerechaConUnderFlow(nodoActual.Hijos[posicionValor]);
                        }
                        //si el derecho no queda en underflow newValue obtiene el mentor de la derecha. 
                        nodoActual.Values[posicionValor] = newValue;
                    }
                    //Si no hay hijo derecho recuperar deIzqADerecha aún si queda en underflow
                    //Mismo método que si ambos quedan en underflow. 
                    nodoActual.Values[posicionValor] = DeIzquierdaADerechaConUnderFlow(nodoActual.Hijos[posicionValor]);
                    
                }
                else //Si el izquierdo no queda en underflow newValue obtiene el valor mayor del lado izquierdo. 
                    nodoActual.Values[posicionValor] = newValue;

                Array.Sort(nodoActual.Values);

                FileStream file = new FileStream(this.ruta, FileMode.Open, FileAccess.Write);
                nodoActual.WriteToFile(file, nodoActual.Id);
                file.Close();

            }



        }

        private bool DeIzquierdaADerecha(int idHijo, ref T newValue)
        {
            Nodo<T> nodoActual = new Nodo<T>(this.max, this.gradoArbol);
            string data = LeerLineaArchivo(idHijo, nodoActual.FixedSizedText, this.ruta);
            nodoActual = convertirStringNodo(data);

            if (nodoActual.CountOfValues-1 < this.min)
                return false;
            else
            {
                if ((nodoActual.Hijos[nodoActual.CountOfValues + 1] == 0))
                {
                    T aux = nodoActual.Values[nodoActual.CountOfValues];
                    nodoActual.Values[nodoActual.CountOfValues] = default;
                    nodoActual.CountOfValues--;

                    newValue = aux;
                    return true;
                }
                else //Si el ultimo valor del nodo tiene hijo derecho. 
                {
                    return DeIzquierdaADerecha(nodoActual.Hijos[nodoActual.CountOfValues + 1], ref newValue);
                }

            }
        }

        private T DeIzquierdaADerechaConUnderFlow(int idHijo)
        {
            Nodo<T> nodoActual = new Nodo<T>(this.max, this.gradoArbol);
            string data = LeerLineaArchivo(idHijo, nodoActual.FixedSizedText, this.ruta);
            nodoActual = convertirStringNodo(data);
            
                if ((nodoActual.Hijos[nodoActual.CountOfValues + 1] == 0))
                {
                    T aux = nodoActual.Values[nodoActual.CountOfValues];
                    nodoActual.Values[nodoActual.CountOfValues] = default;

                    //Queda en underflow y se rebalancea. 
                    if (nodoActual.CountOfValues < this.min)
                    {
                   

                    //1. Pedir prestado a su  hermano izquierdo. 

                    Nodo<T> nodoPadre = new Nodo<T>(this.max, this.gradoArbol);
                    string dataPadre = LeerLineaArchivo(nodoActual.Padre, nodoPadre.FixedSizedText, this.ruta);
                    nodoActual = convertirStringNodo(dataPadre);

                    int idHijoIzquierdo = 0;
                    int pos;
                    //Encontrar la pos del nodo actual en el arreglo de hijos del nodo padre para instansiar el hermsno izquierdo. 
                    for ( pos = 0; pos < nodoPadre.Hijos.Length; pos++)
                    {
                        if (nodoActual.Id == nodoPadre.Hijos[pos])
                        {
                            idHijoIzquierdo = nodoPadre.Hijos[pos - 1];
                            break;

                        }
                    }


                    //instancia del nodo hermano izquierdo de nodoActual. 
                    Nodo<T> nodoHermanoIzquierdo = new Nodo<T>(this.max, this.gradoArbol);
                    string dataIzquierdo = LeerLineaArchivo(idHijoIzquierdo, nodoHermanoIzquierdo.FixedSizedText, this.ruta);
                    nodoActual = convertirStringNodo(dataIzquierdo);

                    if (nodoHermanoIzquierdo.CountOfValues - 1 < this.min)
                    {
                        //unir con el hermano izq
                        // pos -1 es la poscicion de la raíz en comun
                        nodoHermanoIzquierdo.Values[nodoHermanoIzquierdo.CountOfValues] = nodoPadre.Values[pos - 1];
                        nodoHermanoIzquierdo.CountOfValues++;
                        nodoPadre.Values[pos - 1] = default;
                        nodoPadre.CountOfValues--;
                        //juntar valore

                        for (int i = 0; i < nodoActual.CountOfValues; i++)
                        {
                            nodoHermanoIzquierdo.Values[nodoHermanoIzquierdo.CountOfValues + i] = nodoActual.Values[i];
                            nodoHermanoIzquierdo.CountOfValues++;
                            nodoActual.Values[i] = default;
                            nodoActual.CountOfValues--;

                        }
                       
                       //Juntar Hijos
                            nodoPadre.Hijos[nodoPadre.ConteoHijos() - 1] = 0;
                            int hijosDerecho = 0;
                            for (int i = nodoHermanoIzquierdo.ConteoHijos(); i < nodoHermanoIzquierdo.Hijos.Length; i++)
                            {
                                try
                                {
                                    nodoHermanoIzquierdo.Hijos[i] = nodoActual.Hijos[hijosDerecho];
                                    hijosDerecho++;
                                }
                                catch (IndexOutOfRangeException a)
                                {

                                    break;
                                }
                               
                            }


                        FileStream file = new FileStream(this.ruta, FileMode.Open, FileAccess.Write);
                        nodoActual.WriteToFile(file, nodoActual.Id);
                        nodoHermanoIzquierdo.WriteToFile(file, nodoHermanoIzquierdo.Id);
                        nodoPadre.WriteToFile(file, nodoPadre.Id);
                        file.Close();
                    }
                    else
                    {
                        

                        nodoActual.Values[nodoActual.CountOfValues] = nodoPadre.Values[nodoPadre.CountOfValues];
                        nodoActual.CountOfValues++;
                        nodoPadre.Values[nodoPadre.CountOfValues] = nodoHermanoIzquierdo.Values[nodoHermanoIzquierdo.CountOfValues];
                        nodoHermanoIzquierdo.Values[nodoHermanoIzquierdo.CountOfValues] = default;
                        nodoHermanoIzquierdo.CountOfValues--;

                        Array.Sort(nodoActual.Values);

                        FileStream file = new FileStream(this.ruta, FileMode.Open, FileAccess.Write);
                        nodoActual.WriteToFile(file, nodoActual.Id);
                        nodoHermanoIzquierdo.WriteToFile(file, nodoHermanoIzquierdo.Id);
                        nodoPadre.WriteToFile(file, nodoPadre.Id);
                        file.Close();


                    }

                }

                    return aux;
                }
                else //Si el ultimo valor del nodo tiene hijo derecho. 
                {
                    return DeIzquierdaADerechaConUnderFlow(nodoActual.Hijos[nodoActual.CountOfValues + 1] );
                }

            
        }

        private bool DeDerechaAIzquierda(int idHijoDer, ref T newValue)
        {
            Nodo<T> nodoActual = new Nodo<T>(this.max, this.gradoArbol);
            string data = LeerLineaArchivo(idHijoDer, nodoActual.FixedSizedText, this.ruta);
            nodoActual = convertirStringNodo(data);
            if (nodoActual.CountOfValues-1 < this.min)
                return false;
            else
            {
                if ((nodoActual.Hijos[0] == 0))
                {
                    T aux = nodoActual.Values[0];
                    nodoActual.Values[0] = default;
                    nodoActual.CountOfValues--;

                    newValue = aux;
                    return true;
                }
                else //Si el primer valor del nodo tiene hijo izquierdo. 
                {
                    return DeIzquierdaADerecha(nodoActual.Hijos[0], ref newValue);
                }
            }
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

       

      
    }
}

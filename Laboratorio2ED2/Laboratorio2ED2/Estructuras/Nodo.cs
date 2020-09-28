using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.IO;


namespace Laboratorio2ED2
{
    internal class Nodo<T> where T : IComparable
    {
        internal T[] Values { get; set; }

        internal int Id { get; set; }

        internal int Padre { get; set; }

        internal int[] Hijos { get; set; }
        public int CountOfValues { get; set; }

        public int Order { get; set; }

        internal Nodo(int maxValue, int order)
        {
            Values = new T[maxValue];
            Hijos = new int[order];
            CountOfValues = 0;
            Order = order;

        }

        public int FixedSizedText => FixedSize();

        public int ConteoHijos()
        {
            int count = 0;
            for (int i = 0; i < Hijos.Length; i++)
            {
                if (Hijos[i] != 0)
                {
                    count++;
                }
            }
            return count;
        }

        // El valaor del size está a discusión. 
        private int FixedSize()
        {
            /*180 es un aprox de cuantos caracteres puede tener un json, multiplicado por el número 
             * de valores que puede tener un nodo segun el grado que se envíe al arbol. */
            /* int caracteresValores = 180 * (Order - 1);
             return caracteresValores + 1;*/

            return 1026;


        }
        /// <summary>
        /// Convierte el arreglo de enteros de los id de los hijos en un una cadena de caracteres
        /// para poder escribirla en el ToString. 
        /// 
        /// Para semarar el id de los hjos en cada nodo dentro del archivo se usa el caracter "/". 
        /// </summary>
        public string HijosToString()
        {
            string resultado = "";
            for (int i = 0; i < Hijos.Length; i++)
            {
                if (i == Hijos.Length - 1)
                    resultado += $"{Hijos[i]}";
                else
                    resultado += $"{Hijos[i]}/";
            }
            return resultado;
        }
        /// <summary>
        /// Convierte el arreglo de valores <T> en una cadena de 
        /// llena de jsons separados por el caracter "/"
        /// </summary>
        /// <returns></returns>
        public string ValuesToString()
        {
            string resultado = "";

            for (int i = 0; i <= CountOfValues; i++)
            {
                if (CountOfValues == 0) break;
                if (i == Values.Length - 1)
                    resultado += $"{JsonConvert.SerializeObject(Values[i])}";
                else
                    resultado += $"{JsonConvert.SerializeObject(Values[i])}/";
            }
            return resultado;
        }
        public override string ToString()
        {
            return $"{Id}|{Padre}|{HijosToString()}|{ValuesToString()}";
        }

        public string ToFixedLengthString()
        {
            // string caracteresHijos = (8 * Order).ToString();
            //leftp rp
            return $"{Id:00000000;-0000000}|{Padre:00000000;-0000000}|" +
               $"{string.Format("{0,-50}", HijosToString())}|{string.Format("{0,-1000}", ValuesToString())}|{string.Format("{0,2}", CountOfValues.ToString())}\n";

            // string dato = "Hola";
            // dato.PadLeft(5,);
            // return "";
        }

        public void WriteToFile(string ruta, int position)
        {
            //System.IO.FileStream file
            int encabezado = 0;
            FileStream file = new FileStream(ruta, FileMode.Open, FileAccess.Write);
            file.Seek(position * FixedSizedText + encabezado, System.IO.SeekOrigin.Begin);
            file.Write(Encoding.ASCII.GetBytes(ToFixedLengthString()), 0, FixedSizedText);
            file.Flush();
            file.Close();
        }


    }
}

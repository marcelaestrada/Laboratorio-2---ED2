using System;
<<<<<<< HEAD
using Laboratorio2ED2;
=======
using System.IO;
>>>>>>> d64fbcf2298fb7f94923bc2b8868019a763c80cd

namespace Laboratorio2ED2
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
           /* ArbolB<int> arbol = new ArbolB<int>(5, @"./ArchivoPeliculas.txt");
            arbol.Insertar(2, 2);*/

             FileStream file = new FileStream(@"C:\LabEstructurasII\Laboratorio-2---ED2\Laboratorio2ED2\PruebaArbol\FileTest.txt", 
             FileMode.Open, FileAccess.ReadWrite);
             file.Seek(21, System.IO.SeekOrigin.Begin);
            
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine("0|0|0/0/0/0|0/0/0/0");
            file.Seek(21, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(file);
            string respuesta = reader.ReadLine();
            file.Close();
            Console.WriteLine("Editado!");
            Console.WriteLine(respuesta);

            //2|3|0/0/0/0|5/8/9/5




            //Ir a posicion de raiz o nodo en la recursividad. ok
            //Hacer la lectura de archivo y asignar valores al nodo aux1.ok

            //Evaluear si el valor buscado es el mismo en el campo hijos. ok
            //Si no, evaluear menor o mayor el x numero de veces ok

            //Usar el número de validaciones para ir a la posicion del arreglo hijos. ok
            //Recuperar id del nodo hijo al que se movio. 

            //REPETIR RECURCIVIDAD. 

            //Al encontrar el valor evaluar casos de eliminacion. 

        }
    }
}

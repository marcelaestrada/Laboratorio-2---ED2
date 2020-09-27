using System;

/*using Laboratorio2ED2;

using System.IO;*/


namespace Laboratorio2ED2
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ArbolB<int> arbol = new ArbolB<int>(5, @"./ArchivoPeliculas.txt");
            arbol.Insertar(2, 2);

            /* FileStream file = new FileStream(@"C:\LabEstructurasII\Laboratorio-2---ED2\Laboratorio2ED2\PruebaArbol\FileTest.txt", 
             FileMode.Open, FileAccess.ReadWrite);
             file.Seek(21, System.IO.SeekOrigin.Begin);
            
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine("0|0|0/0/0/0|0/0/0/0");
            file.Seek(21, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(file);
            string respuesta = reader.ReadLine();
            file.Close();
            Console.WriteLine("Editado!");
            Console.WriteLine(respuesta);*/

            //2|3|0/0/0/0|5/8/9/5


        }
    }
}

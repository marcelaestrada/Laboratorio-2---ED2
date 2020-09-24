using System;

namespace Laboratorio2ED2
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ArbolB<int> arbol = new ArbolB<int>(5, @"c:\ArchivoPeliculas.txt");
            arbol.Insertar(2, 2);
        }
    }
}

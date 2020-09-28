using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laboratorio2ED2;


namespace API.Helpers
{
    public class Storage
    {
        private static Storage _instance = null;

        public static Storage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Storage();

                return _instance;
            }
        }


        public int id = 1;
        public int gradoA = 0;
        public ArbolB<Pelicula> arbolPeliculas;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace API.Models
{
    public class Pelicula : IComparable
    {
        public string Director { get; set; }
        public double ImdbRating { get; set; }
        public string Genre { get; set; }
        public string ReleaseDate { get; set; }
        public double RottenTomatoesRating { get; set; }
        public string Title { get; set; }

        private string Id;

        public string id
        {
            get { return Title + AñoPelicula(); }
            //set { Id = value; }
        }


       

        private string AñoPelicula()
        {
            string[] recuperarAño = ReleaseDate.Split(" ");
            return recuperarAño[2];
        }



        public int CompareTo(object obj)
        {
            return this.Id.CompareTo(obj.ToString());
        }
    }
}

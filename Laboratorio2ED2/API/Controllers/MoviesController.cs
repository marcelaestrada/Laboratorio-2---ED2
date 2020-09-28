using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Laboratorio2ED2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public int id = 1;
        public static int gradoA = 0;
        public ArbolB<Pelicula> arbolPeliculas = new ArbolB<Pelicula>(gradoA, @".\ArchivoPeliculas.txt");

        // POST: api/<movie>
        [HttpPost]
        public int CrearArbol([FromBody] object grado)
        {
            Orden gradoArbol = JsonConvert.DeserializeObject<Orden>(grado.ToString());
            gradoA = gradoArbol.orden;
            return gradoA;
        }

        //api/movies/populate
        [HttpPost("populate")]
        public async Task<ActionResult> Post([FromBody] object peliculas)
        {
            List<Pelicula> dataSet = JsonConvert.DeserializeObject<List<Pelicula>>(peliculas.ToString());

            try
            {
                foreach (var item in dataSet)
                {
                    id = arbolPeliculas.Insertar(item, id);
                }
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500);
            }


        }
    }
}

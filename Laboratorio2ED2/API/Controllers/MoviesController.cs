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
        int id = 1;
        ArbolB<Pelicula> arbolPeliculas = new ArbolB<Pelicula>()
        //api/movies/populate
        [HttpPost("populate")]
        public async Task<ActionResult> Post([FromBody] object peliculas)
        {
            List<Pelicula> dataSet = JsonConvert.DeserializeObject<List<Pelicula>>(content);

            try
            {
                foreach (var item in dataSet)
                {

                    arbol.Insert(item);

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

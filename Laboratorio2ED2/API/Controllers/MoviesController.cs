using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using API.Models;
using Laboratorio2ED2;
=======
using API.Helpers;
using API.Models;
>>>>>>> 0b54f33d78aa300f49fe6178efe60b45e7edcd97
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
<<<<<<< HEAD
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
=======

       



























        /*
         DELETE
            ● Recibe un id en la ruta (/{id})
            ● Elimina dicho elemento
            ● Devuelve OK si no hubo error
            ● Devuelve NotFound si el Id no existe
            ● Devuelve InternalServerError si hubo error
         */
        //api/movies/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteElement(string id) 
        {
           
            try
            {
                Storage.Instance.arbol.Eliminar(id);
                return Ok();
            }
            catch (Exception)
>>>>>>> 0b54f33d78aa300f49fe6178efe60b45e7edcd97
            {

                return StatusCode(500);
            }
<<<<<<< HEAD


        }
=======
            
        }
        






>>>>>>> 0b54f33d78aa300f49fe6178efe60b45e7edcd97
    }
}

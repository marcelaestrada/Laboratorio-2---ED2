using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Helpers;
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
       

        [HttpGet("{transversal}")]
        public string GetRecorrido(string transversal)
        {
            List<Pelicula> listaPeliculas;
            string jsonPeliculas;

            switch (transversal)
            {
                case "inorden":
                    listaPeliculas = Storage.Instance.arbolPeliculas.InOrder();
                    jsonPeliculas = JsonConvert.SerializeObject(listaPeliculas);
                    return jsonPeliculas;

                case "preorden":
                     listaPeliculas = Storage.Instance.arbolPeliculas.PreOrder();
                     jsonPeliculas = JsonConvert.SerializeObject(listaPeliculas);
                     return jsonPeliculas;

                case "postorden":
                    listaPeliculas = Storage.Instance.arbolPeliculas.PostOrder();
                    jsonPeliculas = JsonConvert.SerializeObject(listaPeliculas);
                    return jsonPeliculas;

                default:
                    return "";
            }
        }

        // POST: api/<movie>
        [HttpPost]
        public int CrearArbol([FromBody] object grado)
        {
            Orden gradoArbol = JsonConvert.DeserializeObject<Orden>(grado.ToString());
            Storage.Instance.gradoA = gradoArbol.orden;
            Storage.Instance.arbolPeliculas = new ArbolB<Pelicula>(Storage.Instance.gradoA, @".\ArchivoPeliculas.txt");
            return Storage.Instance.gradoA;
        }

        //api/movies/populate
        [HttpPost("populate")]
        public async Task<ActionResult> Post([FromForm] IFormFile file)
        {
            using var contentInMemory = new MemoryStream();
            await file.CopyToAsync(contentInMemory);
            var peliculas = Encoding.ASCII.GetString(contentInMemory.ToArray());
            List<Pelicula> dataSet = JsonConvert.DeserializeObject<List<Pelicula>>(peliculas);

            try
            {
                foreach (var item in dataSet)
                {
                    Storage.Instance.id = Storage.Instance.arbolPeliculas.Insertar(item, Storage.Instance.id);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }


        }
      
        //api/movies/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteElement(string id)
        {

            try
            {
                if (!Storage.Instance.arbolPeliculas.Eliminar(id))
                {
                    return NotFound();

                }
                else return Ok();

            }
            catch (Exception)
            {

                return StatusCode(500);
            }

        }


    }
}

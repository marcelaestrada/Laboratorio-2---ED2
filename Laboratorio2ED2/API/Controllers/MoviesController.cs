using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

       



























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
            {

                return StatusCode(500);
            }
            
        }
        






    }
}

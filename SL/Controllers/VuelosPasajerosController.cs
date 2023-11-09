using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{

    [RoutePrefix("api/VuelosPasajeros")]
    public class VuelosPasajerosController : ApiController
    {
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ML.VuelosPasajeros vuelosPasajeros)
        {

            ML.Resultado resultado = BL.VuelosPasajeros.Add(vuelosPasajeros.Vuelos, vuelosPasajeros.Pasajeros);

            if (resultado.Correct)
            {
                return Content(HttpStatusCode.OK, resultado);
            } else
            {
                return Content(HttpStatusCode.BadRequest, resultado);
            }
        }


        [Route("{fechaInicio},{fechaFin}")]
        [HttpGet]
        public IHttpActionResult GetAll(DateTime fechaInicio, DateTime fechaFin)
        {
            ML.Resultado resultado = BL.VuelosPasajeros.GetAll(fechaInicio, fechaFin);

            if (resultado.Correct)
            {
                return Content(HttpStatusCode.OK, resultado);
            } else
            {
                return Content(HttpStatusCode.BadRequest, resultado);
            }
        }

    }
}

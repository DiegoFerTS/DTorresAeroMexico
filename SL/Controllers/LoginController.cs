using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult UserLogin(ML.Usuario usuario)
        {
            ML.Resultado resultado = BL.Login.UserLogin(usuario);

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

using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class VuelosPasajerosController : Controller
    {
        // GET: VuelosPasajeros
        public ActionResult Form()
        {
            ML.VuelosPasajeros vuelosPasajeros = new ML.VuelosPasajeros();
            vuelosPasajeros.Vuelos = new List<ML.Vuelo>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:42407/api/VuelosPasajeros/");

                var requestTask = client.GetAsync("01-01-2000,12-31-9999");
                requestTask.Wait();

                var resultService = requestTask.Result;

                if (resultService.IsSuccessStatusCode)
                {
                    var readResponse = resultService.Content.ReadAsAsync<ML.Resultado>();
                    readResponse.Wait();

                    foreach (var vueloResult in readResponse.Result.Objects)
                    {
                        ML.Vuelo deserializableVuelo = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Vuelo>(vueloResult.ToString());
                        deserializableVuelo.InformacionVuelo = deserializableVuelo.Origen + " - " + deserializableVuelo.Destino + "  " + deserializableVuelo.FechaSalida;
                        vuelosPasajeros.Vuelos.Add(deserializableVuelo);
                    }
                }

            }

            return View(vuelosPasajeros);
        }

        [HttpPost]
        public ActionResult AddVueloPasajero()
        {
            string[] idsPasajero = Request.Form.GetValues("idPasajero");
            string[] numerosVuelo = Request.Form.GetValues("numeroVuelo");

            ML.VuelosPasajeros vuelosPasajeros = new ML.VuelosPasajeros();
            vuelosPasajeros.Vuelos = new List<ML.Vuelo>();
            vuelosPasajeros.Pasajeros = new List<ML.Pasajero>();

            if (idsPasajero.Length == numerosVuelo.Length)
            {

                for (int i = 0; i < idsPasajero.Length; i++)
                {
                    ML.Pasajero pasajero = new ML.Pasajero();
                    ML.Vuelo vuelo = new ML.Vuelo();

                    pasajero.Id = int.Parse(idsPasajero[i]);
                    vuelo.NumeroVuelo = numerosVuelo[i];

                    vuelosPasajeros.Pasajeros.Add(pasajero);
                    vuelosPasajeros.Vuelos.Add(vuelo);
                }


                using (var context = new HttpClient())
                {
                    context.BaseAddress = new Uri("http://localhost:42407/api/VuelosPasajeros");

                    var requestTask = context.PostAsJsonAsync<ML.VuelosPasajeros>("vuelosPasajeros", vuelosPasajeros);
                    requestTask.Wait();

                    var resultTask = requestTask.Result;

                    var readTask = resultTask.Content.ReadAsAsync<ML.Resultado>();
                    readTask.Wait();


                    if (resultTask.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = readTask.Result.Message;
                    } else
                    {
                        ViewBag.Mensaje = readTask.Result.Message;
                    }

                }

            } else
            {
                ViewBag.Mensaje = "No se ingresaron todos los datos.";
            }

            return PartialView("Modal");

        }
    }
}
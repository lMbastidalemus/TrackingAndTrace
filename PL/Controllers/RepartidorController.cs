using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;
using System.Net.Http;

namespace PL.Controllers
{
    public class RepartidorController : Controller
    {
        // GET: Repartidor
        public ActionResult GetAll()
        {
            Repartidor repartidor = new Repartidor();
            repartidor.UnidadEntrega = new UnidadEntrega();
            

            using (var cliente = new HttpClient())
            {
                repartidor.Repartidores = new List<object>();
                cliente.BaseAddress = new Uri("http://localhost:5019/api/Repartidor/");
                var task = cliente.GetAsync("GetAll");
                task.Wait();

                var resultTask = task.Result;
                if (resultTask.IsSuccessStatusCode)
                {
                    var readTask = resultTask.Content.ReadAsAsync<Repartidor>();
                    readTask.Wait();
                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        BL.Repartidor resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<Repartidor>(resultItem.ToString());
                        repartidor.Repartidores.Add(resultItemList);
                    }

                }


                return View(repartidor);
            }
           

        }

        [HttpGet]
        public ActionResult Form(int? IdRepartidor)
        {
            Repartidor repartidor = new Repartidor();
            repartidor.UnidadEntrega = new UnidadEntrega();
            repartidor.UnidadEntrega.EstatusUnidad = new EstatusUnidad();
            UnidadEntrega repartidorEntrega = BL.UnidadEntrega.GetAll();
            EstatusUnidad estatusUnidad = BL.EstatusUnidad.GetAll();

            if (IdRepartidor != null)
            {
                Repartidor repartidorResult = BL.Repartidor.GetById(IdRepartidor.Value);
                if (repartidorResult.Correct)
                {
                    repartidor = (Repartidor)repartidorResult.Object;
                    repartidor.UnidadEntrega.Unidades = repartidorEntrega.Objects;
                    repartidor.UnidadEntrega.EstatusUnidad.EstatusUnidades = estatusUnidad.Objects;
                }
            }
            repartidor.UnidadEntrega.Unidades = repartidorEntrega.Objects;
            repartidor.UnidadEntrega.EstatusUnidad.EstatusUnidades = estatusUnidad.Objects;
            return View(repartidor);

        }

        [HttpPost]
        public ActionResult Form(Repartidor repartidor)
        {

            repartidor.Repartidores = new List<object>();
            if (repartidor.IdRepartidor == 0)
            {
                
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5019/api/Repartidor/");
                    var task = client.PostAsJsonAsync<Repartidor>("Add/", repartidor);
                    task.Wait();

                    var taskResult = task.Result;
                    if (taskResult.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Agregado";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error";
                    }
                }
            }
            else
            {
                using (var client = new HttpClient())
                {



                    client.BaseAddress = new Uri("http://localhost:5019/api/Repartidor/");
                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<BL.Repartidor>("Update/" + repartidor.IdRepartidor, repartidor);
                    postTask.Wait();
                    //h

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Departamento actualizado correctamente";
                       
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error al actualizar departamento";
                      
                    }
                }
            }
            return PartialView("Modal");
        }

        public ActionResult Delete(int IdRepartidor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5019/api/Repartidor/");
                var task = client.DeleteAsync("Delete/" + IdRepartidor);
                task.Wait();

                var result = task.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Repartidor eliminado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Error al eliminar el repartidor";
                    return PartialView("Modal");
                }
            }

        }
    }
}

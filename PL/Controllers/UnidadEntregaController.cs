using Microsoft.AspNetCore.Mvc;
using BL;

namespace PL.Controllers
{
    public class UnidadEntregaController : Controller
    {
        public ActionResult GetAll()
        {
            UnidadEntrega u = new UnidadEntrega();
            //UnidadEntrega unidadEntrega = BL.UnidadEntrega.GetAll();
            //EstatusUnidad result = BL.EstatusUnidad.GetAll();

            using (var cliente = new HttpClient())
            {
                //u.Unidades = unidadEntrega.Objects;
                //u.EstatusUnidad.EstatusUnidades = result.Objects;
                u.Unidades = new List<object>();
                cliente.BaseAddress = new Uri("http://localhost:5019/api/UnidadEntrega/");
                var task = cliente.GetAsync("GetAll");
                task.Wait();

                var resultTask = task.Result;
                if (resultTask.IsSuccessStatusCode)
                {
                    var readTask = resultTask.Content.ReadAsAsync<Repartidor>();
                    readTask.Wait();
                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        BL.UnidadEntrega resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<UnidadEntrega>(resultItem.ToString());
                        u.Unidades.Add(resultItemList);
                    }

                }


                return View(u);
            }
        }

        public ActionResult Form(int? IdUnidad)
        {
            UnidadEntrega unidad = new UnidadEntrega();
            unidad.EstatusUnidad = new EstatusUnidad();
            EstatusUnidad estatusUnidad = BL.EstatusUnidad.GetAll();
            if (IdUnidad != null)
            {
                UnidadEntrega result = BL.UnidadEntrega.GetById(IdUnidad.Value);
                unidad = (UnidadEntrega)result.Object;

                unidad.EstatusUnidad.EstatusUnidades = estatusUnidad.Objects;
            }
            else
            {
                unidad.EstatusUnidad.EstatusUnidades = estatusUnidad.Objects;
            }
            return View(unidad);
        }


        [HttpPost]
        public ActionResult Form(UnidadEntrega unidad)
        {

           
            if (unidad.IdUnidad == 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5019/api/UnidadEntrega/");
                    var task = client.PostAsJsonAsync<UnidadEntrega>("Add", unidad);
                    task.Wait();

                    var taskResult = task.Result;
                    if (taskResult.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Correcto";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error";
                    }
                }
            }
            else
            {
                UnidadEntrega result = BL.UnidadEntrega.Update(unidad);
                if (result.Correct.Value)
                {
                    ViewBag.Mensaje = "Correcto";

                }
                else
                {
                    ViewBag.Mensaje = "Error";
                }
            }

            return PartialView("Modal");
        }
        public ActionResult Delete(int IdUnidad)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5019/api/UnidadEntrega/");
                var task = client.DeleteAsync("Delete" + IdUnidad);
                task.Wait();

                var taskResult = task.Result;
                if (taskResult.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Eliminado correctamente";
                    
                }
                else
                {
                    ViewBag.Mensaje = "Error";
                   
                }
            }
            return PartialView("Modal");

        }

        

    }
}

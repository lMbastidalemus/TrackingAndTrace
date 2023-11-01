using Microsoft.AspNetCore.Mvc;
using BL;

namespace PL.Controllers
{
    public class UnidadEntregaController : Controller
    {
        public ActionResult GetAll()
        {
            UnidadEntrega u = new UnidadEntrega();
            u.EstatusUnidad = BL.EstatusUnidad.GetAll();
            UnidadEntrega unidadEntrega = BL.UnidadEntrega.GetAll();
            EstatusUnidad result =  BL.EstatusUnidad.GetAll();
            u.Unidades = unidadEntrega.Objects;
            u.EstatusUnidad.EstatusUnidades = result.Objects;
            return View(u);
        }
    }
}

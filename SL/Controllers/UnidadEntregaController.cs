using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadEntregaController : ControllerBase
    {
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll ()
        {
            UnidadEntrega result = BL.UnidadEntrega.GetAll();
            if (result.Correct.Value)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Route("GetById/{IdUnidad}")]
        [HttpGet]
        public IActionResult GetById(int IdUnidad)
        {
            UnidadEntrega result = BL.UnidadEntrega.GetById(IdUnidad);
            if (result.Correct.Value)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [Route("Add")]
        [HttpPost]
        public IActionResult Add(UnidadEntrega unidad)
        {
            UnidadEntrega result = BL.UnidadEntrega.Add(unidad);
            if (result.Correct.Value)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Route("Update/{IdUnidad}")]
        [HttpPost]
        public IActionResult Update(int IdUnidad, [FromBody]  UnidadEntrega unidad)
        {
            unidad.IdUnidad = IdUnidad;
            UnidadEntrega result = BL.UnidadEntrega.Update(unidad);
            if (result.Correct.Value)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [Route("Delete/{IdUnidad}")]
        [HttpDelete]
        public IActionResult Delete(int IdUnidad)
        {
            UnidadEntrega result = BL.UnidadEntrega.Delete(IdUnidad);
            if (result.Correct.Value)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}

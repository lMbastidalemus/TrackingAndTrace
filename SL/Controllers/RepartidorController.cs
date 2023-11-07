using BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepartidorController : ControllerBase
    {
        // GET: api/<RepartidorController>

        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            Repartidor repartidor = BL.Repartidor.GetAll();
            if (repartidor.Correct)
            {
                return Ok(repartidor);
            }
            else
            {
                return BadRequest(repartidor);
            }
        }

        [Route("Delete/{IdRepartidor}")]
        [HttpDelete]
        public IActionResult Delete(int IdRepartidor)
        {
            Repartidor repartidor = BL.Repartidor.Delete(IdRepartidor);
            if (repartidor.Correct)
            {
                return Ok(repartidor);
            }
            else
            {
                return BadRequest(repartidor);
            }
        }

        [Route("GetById/{IdRepartidor}")]
        [HttpGet]
        public IActionResult GetById(int IdRepartidor)
        {
            Repartidor repartidor = BL.Repartidor.GetById(IdRepartidor);
            if (repartidor.Correct)
            {
                return Ok(repartidor);
            }
            else
            {
                return BadRequest(repartidor);
            }
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult Add(Repartidor repartidor)
        {
            Repartidor repartidorResult = BL.Repartidor.Add(repartidor);
            if (repartidorResult.Correct)
            {
                return Ok(repartidorResult);
            }
            else
            {
                return BadRequest(repartidorResult);
            }
        }

        [Route("Update/{IdRepartidor}")]
        [HttpPut]
        public IActionResult Update(int IdRepartidor, [FromBody] Repartidor repartidor)
        {
            repartidor.IdRepartidor = IdRepartidor;
            Repartidor repartidorResult = BL.Repartidor.Update(repartidor);
            if (repartidorResult.Correct)
            {
                return Ok(repartidorResult);
            }
            else
            {
                return BadRequest(repartidorResult);
            }
        }
    }
}

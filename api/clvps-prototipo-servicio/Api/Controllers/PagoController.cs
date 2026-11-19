using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace clvps_prototipo_servicio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly PagoService _pagoService;

        public PagoController(PagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userName, long orden)
        {
            PagoDetalleResponse result = await _pagoService.GetPagoDetalle(userName, orden);

            if (result.Ok)
            {
                return Ok(result);
            }

            return NotFound(new { result.ErrorMessage });
        }

        [HttpGet("pagar")]
        public async Task<IActionResult> Pagar(string userName, long orden)
        {
            PagoConfirmacionResponse result = await _pagoService.Pagar(userName, orden);

            if (result.Ok)
            {
                return Ok(result);
            }

            return StatusCode(500, new { result.ErrorMessage });
        }
    }
}

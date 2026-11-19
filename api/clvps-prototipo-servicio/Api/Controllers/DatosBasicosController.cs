using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace clvps_prototipo_servicio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosBasicosController : ControllerBase
    {
        private readonly DatosBasicosService _datosBasicosService;

        public DatosBasicosController(DatosBasicosService datosBasicosService)
        {
            _datosBasicosService = datosBasicosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userName)
        {
            DatosBasicosResponse result = await _datosBasicosService.GetDatosBasicos(userName);

            if (result.Ok)
            {
                return Ok(result);
            }

            return NotFound(new { result.ErrorMessage });
        }
    }
}

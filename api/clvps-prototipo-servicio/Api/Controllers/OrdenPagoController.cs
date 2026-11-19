using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace clvps_prototipo_servicio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenPagoController : ControllerBase
    {
        private readonly OrdenPagoService _ordenPagoService;

        public OrdenPagoController(OrdenPagoService ordenPagoService)
        {
            _ordenPagoService = ordenPagoService;
        }

        // POST api/<OrdenPagoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdenPagoRequest request)
        {
            OrdenPagoResponse result = await _ordenPagoService.CreateOrdenPago(request);

            if (result.Ok)
            {
                return Ok(result);
            }

            return StatusCode(500, new { result.ErrorMessage });
        }

        // PUT api/<OrdenPagoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // GET: api/<OrdenPagoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrdenPagoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // DELETE api/<OrdenPagoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

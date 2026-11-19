using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Entities;
using clvps_prototipo_servicio.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace clvps_prototipo_servicio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string userName)
        {
            List<TransactionsListaItemResponse> transactions = await _transactionService.GetTransactionsByUser(userName);
            return Ok(transactions);
        }


        [HttpGet("pago")]
        public async Task<IActionResult> GetTransPago(long id)
        {
            TransactionPagoResponse result = await _transactionService.GetTransactionPago(id);

            if (result.Ok)
            {
                return Ok(result);
            }

            return NotFound(new { result.ErrorMessage });
        }

        [HttpGet("recarga")]
        public async Task<IActionResult> GetTransRecarga(long id)
        {
            TransactionRecargaResponse result = await _transactionService.GetTransactionRecarga(id);

            if (result.Ok)
            {
                return Ok(result);
            }

            return NotFound(new { result.ErrorMessage });
        }

        [HttpGet("retiro")]
        public async Task<IActionResult> GetTransRetiro(long id)
        {
            TransactionRetiroResponse result = await _transactionService.GetTransactionRetiro(id);

            if (result.Ok)
            {
                return Ok(result);
            }

            return NotFound(new { result.ErrorMessage });
        }
    }
}

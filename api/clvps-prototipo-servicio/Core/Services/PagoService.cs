using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Interfaces;

namespace clvps_prototipo_servicio.Core.Services
{
    public class PagoService
    {
        private readonly IPagoRepository _pagoRepository;
        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        public async Task<PagoDetalleResponse> GetPagoDetalle(string userName, long idOrden)
        {
            return await _pagoRepository.GetPagoDetalle(userName, idOrden);
        }

        public async Task<PagoConfirmacionResponse> Pagar(string userName, long idOrden)
        {
            return await _pagoRepository.Pagar(userName, idOrden);
        }
    }
}

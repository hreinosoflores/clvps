using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Interfaces;

namespace clvps_prototipo_servicio.Core.Services
{
    public class OrdenPagoService
    {
        private readonly IOrdenPagoRepository _ordenPagoRepository;
        public OrdenPagoService(IOrdenPagoRepository ordenPagoRepository)
        {
            _ordenPagoRepository = ordenPagoRepository;
        }
        public async Task<List<OrdenPagoResponse>> GetOrdenesPagoByUser(string userName)
        {
            return await _ordenPagoRepository.GetOrdenesPagoByUser(userName);
        }
        public async Task<OrdenPagoResponse> GetOrdenPago(long id)
        {
            return await _ordenPagoRepository.GetOrdenPago(id);
        }
        public async Task<OrdenPagoResponse> CreateOrdenPago(OrdenPagoRequest request)
        {
            return await _ordenPagoRepository.CreateOrdenPago(request);
        }
        public async Task<OrdenPagoResponse> UpdateOrdenPago(OrdenPagoRequest request)
        {
            return await _ordenPagoRepository.UpdateOrdenPago(request);
        }
        public async Task<OrdenPagoResponse> DeleteOrdenPago(long id)
        {
            return await _ordenPagoRepository.DeleteOrdenPago(id);
        }

    }
}

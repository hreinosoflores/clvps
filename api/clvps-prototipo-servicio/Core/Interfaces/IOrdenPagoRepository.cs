using clvps_prototipo_servicio.Api.DTO;

namespace clvps_prototipo_servicio.Core.Interfaces
{
    public interface IOrdenPagoRepository
    {
        Task<List<OrdenPagoResponse>> GetOrdenesPagoByUser(string userName);
        Task<OrdenPagoResponse> GetOrdenPago(long id);
        Task<OrdenPagoResponse> CreateOrdenPago(OrdenPagoRequest ordenPago);
        Task<OrdenPagoResponse> UpdateOrdenPago(OrdenPagoRequest ordenPago);
        Task<OrdenPagoResponse> DeleteOrdenPago(long id);
    }
}

using clvps_prototipo_servicio.Api.DTO;

namespace clvps_prototipo_servicio.Core.Interfaces
{
    public interface IPagoRepository
    {
        Task<PagoDetalleResponse> GetPagoDetalle(string userName, long idOrden);

        Task<PagoConfirmacionResponse> Pagar(string userName, long idOrden);
    }
}

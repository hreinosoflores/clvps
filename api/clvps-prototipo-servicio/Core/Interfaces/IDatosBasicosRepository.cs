using clvps_prototipo_servicio.Api.DTO;

namespace clvps_prototipo_servicio.Core.Interfaces
{
    public interface IDatosBasicosRepository
    {
        Task<DatosBasicosBuyerResponse> GetDatosBasicosBuyer(string userName);
        Task<DatosBasicosSellerResponse> GetDatosBasicosSeller(string userName);
    }
}

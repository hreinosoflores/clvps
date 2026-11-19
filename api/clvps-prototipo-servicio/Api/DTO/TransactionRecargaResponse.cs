using clvps_prototipo_servicio.Core.Entities;

namespace clvps_prototipo_servicio.Api.DTO
{
    public class TransactionRecargaResponse: TransactionResponse
    {
        public Recarga? Recarga { get; set; }
    }
}

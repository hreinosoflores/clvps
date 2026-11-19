using clvps_prototipo_servicio.Core.Entities;

namespace clvps_prototipo_servicio.Api.DTO
{
    public class TransactionRetiroResponse: TransactionResponse
    {
        public SolicitudRetiro? SolicitudRetiro { get; set; }
    }
}

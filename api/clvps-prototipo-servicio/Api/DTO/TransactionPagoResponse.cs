using clvps_prototipo_servicio.Core.Entities;

namespace clvps_prototipo_servicio.Api.DTO
{
    public class TransactionPagoResponse: TransactionResponse
    {
        public OrdenPago? OrdenPago { get; set; }
        public string? Tipo { get; set; }
    }
}

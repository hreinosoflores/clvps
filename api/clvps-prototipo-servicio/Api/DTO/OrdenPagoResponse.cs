using clvps_prototipo_servicio.Core.Entities;

namespace clvps_prototipo_servicio.Api.DTO
{
    public class OrdenPagoResponse
    {
        public long? Id { get; set; }
        public double? Monto { get; set; }
        public string? Estado { get; set; }
        public string? Fecha { get; set; }
        public string? Hora { get; set; }
        public Buyer? Buyer { get; set; }
        public Seller? Seller { get; set; }
        public required bool Ok { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

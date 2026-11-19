using clvps_prototipo_servicio.Core.Entities;

namespace clvps_prototipo_servicio.Api.DTO
{
    public class OrdenPagoRequest
    {
        public long? Id { get; set; }
        public required double Monto { get; set; }
        public required string Usr_s { get; set; }
    }
}

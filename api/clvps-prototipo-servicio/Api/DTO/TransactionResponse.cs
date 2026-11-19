using clvps_prototipo_servicio.Core.Entities;

namespace clvps_prototipo_servicio.Api.DTO
{
    public class TransactionResponse
    {
        public long? Id { get; set; }
        public VirtualAccount? VirtualAccount { get; set; }
        public double? Monto { get; set; }
        public string? Fecha { get; set; }
        public string? Hora { get; set; }
        public bool Ok { get; set; }
        public string? ErrorMessage { get; set; }






      




    
    }
}

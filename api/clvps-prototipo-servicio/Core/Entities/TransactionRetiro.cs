using Microsoft.AspNetCore.Identity;

namespace clvps_prototipo_servicio.Core.Entities
{
    public class TransactionRetiro
    {
        public long Id { get; set; }
        public required long Id_va { get; set; }
        public required string Id_user { get; set; }
        public virtual VirtualAccount? VirtualAccount { get; set; }
        public required long Id_retiro { get; set; }
        public virtual SolicitudRetiro? SolicitudRetiro { get; set; }
        public required double Monto { get; set; }
        public string Fecha { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string Hora { get; set; } = DateTime.Now.ToString("HH:mm:ss");
    }
}



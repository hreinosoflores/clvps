using Microsoft.AspNetCore.Identity;

namespace clvps_prototipo_servicio.Core.Entities
{
    public class TransactionRecarga
    {
        public long Id { get; set; }
        public required long Id_va { get; set; }
        public required string Id_user { get; set; }
        public virtual VirtualAccount? VirtualAccount { get; set; }
        public required long Id_rec { get; set; }
        public virtual Recarga? Recarga { get; set; }
        public required double Monto { get; set; }
        public string Fecha { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string Hora { get; set; } = DateTime.Now.ToString("HH:mm:ss");
    }
}


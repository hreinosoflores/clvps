using Microsoft.AspNetCore.Identity;

namespace clvps_prototipo_servicio.Core.Entities
{
    public class VirtualAccount
    {
        public long Id { get; set; }
        public required string  Id_user { get; set; }
        public virtual IdentityUser? User { get; set; }
        public required string Nro_cuenta { get; set; }
        public double Saldo_soles { get; set; }
        public double Saldo_dolares { get; set; }

    }
}

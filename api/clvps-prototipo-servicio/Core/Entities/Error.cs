using Microsoft.AspNetCore.Identity;

namespace clvps_prototipo_servicio.Core.Entities
{
    public class Error
    {
        public long? Id { get; set; }
        public DateTime Fecha_registro { get; set; } = DateTime.Now;
        public required string Desc_corta { get; set; }
        public string? Desc_detallada { get; set; }
        public string? Id_user { get; set; }
        public virtual IdentityUser? User { get; set; }
    }
}

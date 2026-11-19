using Microsoft.AspNetCore.Identity;

namespace clvps_prototipo_servicio.Core.Entities
{
    public class Seller
    {
        public required string Id { get; set; }
        public virtual IdentityUser? User { get; set; }
        public required string Raz_social { get; set; }
        public required string RUC { get; set; }
    }
}

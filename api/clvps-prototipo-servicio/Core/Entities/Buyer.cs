using Microsoft.AspNetCore.Identity;

namespace clvps_prototipo_servicio.Core.Entities
{
    public class Buyer
    {
        public required string Id { get; set; }
        public virtual IdentityUser? User { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public string? DNI { get; set; }
        public string? CAR_EXT { get; set; }
    }   
}

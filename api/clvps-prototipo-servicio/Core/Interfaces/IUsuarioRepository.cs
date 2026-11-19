using clvps_prototipo_servicio.Core.Entities;

namespace clvps_prototipo_servicio.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<string> GetRol(string userName);
    }
}

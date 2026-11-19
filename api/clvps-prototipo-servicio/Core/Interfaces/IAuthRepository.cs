using clvps_prototipo_servicio.Api.DTO;
using Microsoft.AspNetCore.Identity;

namespace clvps_prototipo_servicio.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponse> Login(LoginRequest request);       
    }
}

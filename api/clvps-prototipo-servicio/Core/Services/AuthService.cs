using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace clvps_prototipo_servicio.Core.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await _authRepository.Login(request);
        }
    }
}

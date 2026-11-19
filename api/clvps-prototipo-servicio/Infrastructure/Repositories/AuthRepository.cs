using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Entities;
using clvps_prototipo_servicio.Core.Interfaces;
using clvps_prototipo_servicio.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace clvps_prototipo_servicio.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CLVPSContext _dbContext;

        public AuthRepository(SignInManager<IdentityUser> signInManager, CLVPSContext dbContext)
        {
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            LoginResponse response = new() { Ok = false };

            var user = await _signInManager.UserManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.ErrorMessage = "No se pudo ingresar al sistema. Inténtelo de nuevo o comúniquese con el administrador";

                var error = new Error
                {
                    Desc_corta = "No se encontró usuario",
                };
                await InsertErrorAsync(error);

                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.ErrorMessage = "No se pudo ingresar al sistema. Inténtelo de nuevo o comúniquese con el administrador";

                var error = new Error
                {
                    Desc_corta = "Credenciales inválidas",
                    Id_user = user.Id
                };

                await InsertErrorAsync(error);
                return response;
            }

            var roles = await _signInManager.UserManager.GetRolesAsync(user);
            response.Ok = true;
            response.UserName = user.UserName;
            response.Rol = roles.ElementAt(0);

            if (response.Rol == "Buyer")
            {
                var buyer = await _dbContext.Buyers.FindAsync(user.Id);
                response.NombreCompleto = $"{buyer!.Nombres} {buyer!.Apellidos}";
            }
            else if (response.Rol == "Seller")
            {
                var seller = await _dbContext.Sellers.FindAsync(user.Id);
                response.NombreCompleto = seller!.Raz_social;
            }

            return response;
        }

        private async Task InsertErrorAsync(Error error)
        {
            await _dbContext.Errors.AddAsync(error);
            await _dbContext.SaveChangesAsync();
        }
    }
}

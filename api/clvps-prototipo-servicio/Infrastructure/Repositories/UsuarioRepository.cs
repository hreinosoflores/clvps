using Azure;
using clvps_prototipo_servicio.Core.Entities;
using clvps_prototipo_servicio.Core.Interfaces;
using clvps_prototipo_servicio.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace clvps_prototipo_servicio.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CLVPSContext _dbContext;
        public UsuarioRepository(SignInManager<IdentityUser> signInManager, CLVPSContext dbContext)
        {
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<string> GetRol(string userName)
        {
            string response;

            var user = await _signInManager.UserManager.FindByNameAsync(userName);

            if (user == null)
            {
                response = "No se pudo obtener rol. Inténtelo de nuevo o comúniquese con el administrador";

                var error = new Error
                {
                    Desc_corta = "No se encontró usuario",
                };
                await InsertErrorAsync(error);
                return response;
            }

            var roles = await _signInManager.UserManager.GetRolesAsync(user);
            response = roles.ElementAt(0);
            return response;
        }

        private async Task InsertErrorAsync(Error error)
        {
            await _dbContext.Errors.AddAsync(error);
            await _dbContext.SaveChangesAsync();
        }
    }
}

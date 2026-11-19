using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Entities;
using clvps_prototipo_servicio.Core.Interfaces;
using clvps_prototipo_servicio.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace clvps_prototipo_servicio.Infrastructure.Repositories
{
    public class DatosBasicosRepository: IDatosBasicosRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CLVPSContext _dbContext;
        public DatosBasicosRepository(SignInManager<IdentityUser> signInManager, CLVPSContext dbContext)
        {
            _signInManager = signInManager;
            _dbContext = dbContext;
        }   

        public async Task<DatosBasicosBuyerResponse> GetDatosBasicosBuyer(string userName)
        {
            DatosBasicosBuyerResponse response = new() { Ok = false };

            var user = await _signInManager.UserManager.FindByNameAsync(userName);

            if (user == null) {
                response.ErrorMessage = "No se pudo obtener usuario. Inténtelo de nuevo o comúniquese con el administrador";

                var error = new Error
                {
                    Desc_corta = "No se encontró usuario",
                };
                await InsertErrorAsync(error);
                return response;
            }

            var buyer = await _dbContext.Buyers.FindAsync(user.Id);
            
            var virtualAccount = await _dbContext.VirtualAccounts.FirstOrDefaultAsync(va => va.Id_user == user.Id);

            if (virtualAccount == null) {
                response.ErrorMessage = "No se pudo obtener cuenta. Inténtelo de nuevo o comúniquese con el administrador";
                var error = new Error
                {
                    Desc_corta = "No se encontró cuenta virtual",
                    Id_user = user.Id
                };
                await InsertErrorAsync(error);
                return response;
            }

            response.Ok = true;
            response.ID_CLVPS = user.Id;
            response.Correo = user.Email;
            response.Telefono = user.PhoneNumber;
            response.Nombres = buyer!.Nombres;
            response.Apellidos = buyer!.Apellidos;
            response.Nro_dni = buyer!.DNI;
            response.Nro_carext = buyer!.CAR_EXT;
            response.Nro_cuenta = virtualAccount.Nro_cuenta;
            response.Saldo_soles = virtualAccount.Saldo_soles;
            response.Saldo_dolares = virtualAccount.Saldo_dolares;

            return response;

        }

        public async Task<DatosBasicosSellerResponse> GetDatosBasicosSeller(string userName)
        {
            DatosBasicosSellerResponse response = new() { Ok = false };

            var user = await _signInManager.UserManager.FindByNameAsync(userName);

            if (user == null)
            {
                response.ErrorMessage = "No se pudo obtener usuario. Inténtelo de nuevo o comúniquese con el administrador";

                var error = new Error
                {
                    Desc_corta = "No se encontró usuario",
                };
                await InsertErrorAsync(error);
                return response;
            }

            var seller = await _dbContext.Sellers.FindAsync(user.Id);

            var virtualAccount = await _dbContext.VirtualAccounts.FirstOrDefaultAsync(va => va.Id_user == user.Id);

            if (virtualAccount == null)
            {
                response.ErrorMessage = "No se pudo obtener cuenta. Inténtelo de nuevo o comúniquese con el administrador";
                var error = new Error
                {
                    Desc_corta = "No se encontró cuenta virtual",
                    Id_user = user.Id
                };
                await InsertErrorAsync(error);
                return response;
            }

            response.Ok = true;
            response.ID_CLVPS = user.Id;
            response.Correo = user.Email;
            response.Telefono = user.PhoneNumber;
            response.Razon_social = seller!.Raz_social;
            response.Ruc = seller!.RUC;
            response.Nro_cuenta = virtualAccount.Nro_cuenta;
            response.Saldo_soles = virtualAccount.Saldo_soles;
            response.Saldo_dolares = virtualAccount.Saldo_dolares;

            return response;

        }

        private async Task InsertErrorAsync(Error error)
        {
            await _dbContext.Errors.AddAsync(error);
            await _dbContext.SaveChangesAsync();
        }
    }
}

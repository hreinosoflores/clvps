using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Entities;
using clvps_prototipo_servicio.Core.Interfaces;
using clvps_prototipo_servicio.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace clvps_prototipo_servicio.Infrastructure.Repositories
{
    public class OrdenPagoRepository : IOrdenPagoRepository
    {
        private readonly CLVPSContext _dbContext;
        private readonly SignInManager<IdentityUser> _signInManager;

        public OrdenPagoRepository(CLVPSContext dbContext, SignInManager<IdentityUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        public async Task<OrdenPagoResponse> CreateOrdenPago(OrdenPagoRequest request)
        {
            OrdenPagoResponse response = new() { Ok = false };

            var userSeller = await _signInManager.UserManager.FindByNameAsync(request.Usr_s);

            if (userSeller == null)
            {
                response.ErrorMessage = "No se pudo crear la orden de pago. Inténtelo de nuevo o comúniquese con el administrador";

                var error = new Error
                {
                    Desc_corta = "No se encontró usuario",
                };
                await InsertErrorAsync(error);

                return response;
            }


            var result = _dbContext.OrdenesPago.Add(new OrdenPago
            {
                Monto = request.Monto,
                Estado = "P",
                Id_s = userSeller.Id
            });

            int resp = await _dbContext.SaveChangesAsync();

            if (resp == 0)
            {
                response.ErrorMessage = "No se pudo crear la orden de pago. Inténtelo de nuevo o comúniquese con el administrador";

                var error = new Error
                {
                    Desc_corta = "No se registró orden de pago",
                };

                await InsertErrorAsync(error);
                return response;
            }

            response.Ok = true;
            response.Id = result.Entity.Id;
            response.Monto = result.Entity.Monto;
            response.Estado = result.Entity.Estado;
            response.Fecha = result.Entity.Fecha;
            response.Hora = result.Entity.Hora;

            return response;
        }

        public Task<OrdenPagoResponse> DeleteOrdenPago(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrdenPagoResponse>> GetOrdenesPagoByUser(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<OrdenPagoResponse> GetOrdenPago(long id)
        {
            throw new NotImplementedException();
        }



        public Task<OrdenPagoResponse> UpdateOrdenPago(OrdenPagoRequest ordenPago)
        {
            throw new NotImplementedException();
        }




        private async Task InsertErrorAsync(Error error)
        {
            await _dbContext.Errors.AddAsync(error);
            await _dbContext.SaveChangesAsync();
        }
    }
}

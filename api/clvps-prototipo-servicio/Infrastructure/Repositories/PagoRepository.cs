using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Entities;
using clvps_prototipo_servicio.Core.Interfaces;
using clvps_prototipo_servicio.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace clvps_prototipo_servicio.Infrastructure.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CLVPSContext _dbContext;
        private readonly IConfiguration _configuration;

        public PagoRepository(SignInManager<IdentityUser> signInManager, CLVPSContext dbContext, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<PagoDetalleResponse> GetPagoDetalle(string userName, long idOrden)
        {
            PagoDetalleResponse response = new() { Ok = false };

            var orden = await _dbContext.OrdenesPago
                .Include(o => o.Seller)
                .FirstOrDefaultAsync(o => o.Id == idOrden);

            if (orden == null)
            {
                response.ErrorMessage = "No se pudo obtener orden de pago. Inténtelo de nuevo o comúniquese con el administrador";
                var error = new Error
                {
                    Desc_corta = "No se encontró orden de pago"
                };
                await InsertErrorAsync(error);
                return response;
            }

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

            var account = await _dbContext.VirtualAccounts.FirstOrDefaultAsync(va => va.Id == 1 && va.Id_user == user.Id);
            if (account == null)
            {
                response.ErrorMessage = "No se pudo obtener cuenta virtual. Inténtelo de nuevo o comúniquese con el administrador";
                var error = new Error
                {
                    Desc_corta = "No se encontró cuenta virtual",
                };
                await InsertErrorAsync(error);
                return response;
            }

            response.Ok = true;
            response.Beneficiario = orden.Seller?.Raz_social;
            response.IdOrden = orden.Id;
            response.NroCuenta = account.Nro_cuenta;
            response.SaldoCuenta = account.Saldo_soles;
            response.MontoPagar = orden.Monto;

            return response;
        }

        public async Task<PagoConfirmacionResponse> Pagar(string userName, long idOrden)
        {
            PagoConfirmacionResponse response = new() { Ok = false };

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var options = new DbContextOptionsBuilder<CLVPSContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;

            using var context = new CLVPSContext(options);
            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var orden = await context.OrdenesPago
                    .Include(o => o.Seller)
                    .FirstOrDefaultAsync(o => o.Id == idOrden && o.Estado == "P");
                if (orden == null)
                {
                    await transaction.RollbackAsync();
                    response.ErrorMessage = "Ocurrió un error durante el pago. Inténtelo de nuevo o comúniquese con el administrador";
                    var error = new Error
                    {
                        Desc_corta = "No se encontró orden de pago u orden de pago ya fue pagada"
                    };
                    await InsertErrorAsync(error);
                    return response;
                }

                var user = await _signInManager.UserManager.FindByNameAsync(userName);
                if (user == null)
                {
                    await transaction.RollbackAsync();
                    response.ErrorMessage = "Ocurrió un error durante el pago. Inténtelo de nuevo o comúniquese con el administrador";
                    var error = new Error
                    {
                        Desc_corta = "No se encontró usuario",
                    };
                    await InsertErrorAsync(error);
                    return response;
                }

                var buyerAccount = await context.VirtualAccounts.FirstOrDefaultAsync(va => va.Id == 1 && va.Id_user == user.Id);
                if (buyerAccount == null)
                {
                    await transaction.RollbackAsync();
                    response.ErrorMessage = "Ocurrió un error durante el pago. Inténtelo de nuevo o comúniquese con el administrador";
                    var error = new Error
                    {
                        Desc_corta = "No se encontró cuenta virtual",
                    };
                    await InsertErrorAsync(error);
                    return response;
                }

                var sellerAccount = await context.VirtualAccounts.FirstOrDefaultAsync(va => va.Id == 1 && va.Id_user == orden.Id_s);
                if (sellerAccount == null)
                {
                    await transaction.RollbackAsync();
                    response.ErrorMessage = "Ocurrió un error durante el pago. Inténtelo de nuevo o comúniquese con el administrador";
                    var error = new Error
                    {
                        Desc_corta = "No se encontró cuenta virtual del vendedor",
                    };
                    await InsertErrorAsync(error);
                    return response;
                }

                var transactionPago1 = new TransactionPago
                {
                    Id_va = 1,
                    Id_user = user.Id,
                    Id_ord = idOrden,
                    Tipo = "1",
                    Monto = orden.Monto
                };
                await context.TransactionsPago.AddAsync(transactionPago1);

                var transactionPago2 = new TransactionPago
                {
                    Id_va = 1,
                    Id_user = orden.Id_s,
                    Id_ord = idOrden,
                    Tipo = "2",
                    Monto = orden.Monto
                };
                await context.TransactionsPago.AddAsync(transactionPago2);

                buyerAccount.Saldo_soles -= orden.Monto;
                buyerAccount.Saldo_dolares = buyerAccount.Saldo_soles / 3.628;
                context.VirtualAccounts.Update(buyerAccount);

                sellerAccount.Saldo_soles += orden.Monto;
                sellerAccount.Saldo_dolares = sellerAccount.Saldo_soles / 3.628;
                context.VirtualAccounts.Update(sellerAccount);

                orden.Estado = "O";
                orden.Id_b = user.Id;
                context.OrdenesPago.Update(orden);

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                response.Ok = true;
                response.IdOrden = orden.Id;
                response.Beneficiario = orden.Seller?.Raz_social;
                response.MontoPagado = orden.Monto;
                response.FechaTransaccion = transactionPago2.Fecha;
                response.HoraTransaccion = transactionPago2.Hora;
                response.EmailBuyer = user.Email;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                response.ErrorMessage = "Ocurrió un error durante el pago. Inténtelo de nuevo o comúniquese con el administrador";
                var error = new Error
                {
                    Desc_corta = "Error en la transacción",
                    Desc_detallada = ex.Message
                };
                await InsertErrorAsync(error);
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

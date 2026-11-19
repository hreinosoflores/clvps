using Azure;
using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Entities;
using clvps_prototipo_servicio.Core.Interfaces;
using clvps_prototipo_servicio.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace clvps_prototipo_servicio.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CLVPSContext _dbContext;
        public TransactionRepository(SignInManager<IdentityUser> signInManager, CLVPSContext dbContext)
        {
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<List<TransactionsListaItemResponse>> GetTransactionsByUser(string userName)
        {
            List<TransactionsListaItemResponse> response = new List<TransactionsListaItemResponse>();

            List<TransactionPago> transactionsPago = await GetTransactionsPagoByUser(userName);

            foreach (TransactionPago transaction in transactionsPago)
            {
                response.Add(new TransactionsListaItemResponse
                {
                    Id = transaction.Id,
                    Monto = transaction.Monto,
                    Fecha = transaction.Fecha,
                    Hora = transaction.Hora,
                    Id_ord = transaction.Id_ord,
                    Tipo = transaction.Tipo,
                });
            }

            List<TransactionRecarga> transactionsRecarga = await GetTransactionsRecargaByUser(userName);

            foreach (TransactionRecarga transaction in transactionsRecarga)
            {
                response.Add(new TransactionsListaItemResponse
                {
                    Id = transaction.Id,
                    Monto = transaction.Monto,
                    Fecha = transaction.Fecha,
                    Hora = transaction.Hora,
                    Id_rec = transaction.Id_rec
                });
            }


            List<TransactionRetiro> transactionsRetiro = await GetTransactionsRetiroByUser(userName);

            foreach (TransactionRetiro transaction in transactionsRetiro)
            {
                response.Add(new TransactionsListaItemResponse
                {
                    Id = transaction.Id,
                    Monto = transaction.Monto,
                    Fecha = transaction.Fecha,
                    Hora = transaction.Hora,
                    Id_retiro = transaction.Id_retiro
                });
            }

            return response;
        }

        public async Task<TransactionPagoResponse> GetTransactionPago(long id)
        {
            TransactionPagoResponse response = new() { Ok = false };

            var tran = await _dbContext.TransactionsPago
                .Include(t => t.VirtualAccount)
                .Include(t => t.OrdenPago)
                    .ThenInclude(o => o!.Seller)
                .Include(t => t.OrdenPago)
                    .ThenInclude(o => o!.Buyer)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tran == null)
            {
                response.ErrorMessage = "No se pudo obtener transacción. Inténtelo de nuevo o comúniquese con el administrador";
                var error = new Error
                {
                    Desc_corta = "No se encontró transacción"
                };
                await InsertErrorAsync(error);
                return response;
            }

            response.Ok = true;
            response.Id = tran.Id;
            response.VirtualAccount = tran.VirtualAccount;
            response.Monto = tran.Monto;
            response.Fecha = tran.Fecha;
            response.Hora = tran.Hora;
            response.OrdenPago = tran.OrdenPago;
            response.Tipo = tran.Tipo;

            return response;
        }

        public async Task<TransactionRecargaResponse> GetTransactionRecarga(long id)
        {
            TransactionRecargaResponse response = new() { Ok = false };

            var tran = await _dbContext.TransactionsRecarga
                .Include(t => t.VirtualAccount)
                .Include(t => t.Recarga)
                    .ThenInclude(o => o!.ExchangeSpot)
                .Include(t => t.Recarga)
                    .ThenInclude(o => o!.SystemManager)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tran == null)
            {
                response.ErrorMessage = "No se pudo obtener transacción. Inténtelo de nuevo o comúniquese con el administrador";
                var error = new Error
                {
                    Desc_corta = "No se encontró transacción"
                };
                await InsertErrorAsync(error);
                return response;
            }

            response.Ok = true;
            response.Id = tran.Id;
            response.VirtualAccount = tran.VirtualAccount;
            response.Monto = tran.Monto;
            response.Fecha = tran.Fecha;
            response.Hora = tran.Hora;
            response.Recarga = tran.Recarga;

            return response;
        }

        public async Task<TransactionRetiroResponse> GetTransactionRetiro(long id)
        {
            TransactionRetiroResponse response = new() { Ok = false };

            var tran = await _dbContext.TransactionsRetiro
                .Include(t => t.VirtualAccount)
                .Include(t => t.SolicitudRetiro)
                    .ThenInclude(o => o!.Seller)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tran == null)
            {
                response.ErrorMessage = "No se pudo obtener transacción. Inténtelo de nuevo o comúniquese con el administrador";
                var error = new Error
                {
                    Desc_corta = "No se encontró transacción"
                };
                await InsertErrorAsync(error);
                return response;
            }

            response.Ok = true;
            response.Id = tran.Id;
            response.VirtualAccount = tran.VirtualAccount;
            response.Monto = tran.Monto;
            response.Fecha = tran.Fecha;
            response.Hora = tran.Hora;
            response.SolicitudRetiro = tran.SolicitudRetiro;

            return response;
        }



        private async Task<List<TransactionPago>> GetTransactionsPagoByUser(string userName)
        {
            List<TransactionPago> transactions = new List<TransactionPago>();
            var user = await _signInManager.UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                var error = new Error
                {
                    Desc_corta = "No se encontró usuario",
                };
                await InsertErrorAsync(error);
                return transactions;
            }

            transactions = await _dbContext.TransactionsPago.Where(t => t.Id_user == user.Id).ToListAsync();
            return transactions;
        }

        private async Task<List<TransactionRecarga>> GetTransactionsRecargaByUser(string userName)
        {
            List<TransactionRecarga> transactions = new List<TransactionRecarga>();
            var user = await _signInManager.UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                var error = new Error
                {
                    Desc_corta = "No se encontró usuario",
                };
                await InsertErrorAsync(error);
                return transactions;
            }

            transactions = await _dbContext.TransactionsRecarga.Where(t => t.Id_user == user.Id).ToListAsync();
            return transactions;
        }

        private async Task<List<TransactionRetiro>> GetTransactionsRetiroByUser(string userName)
        {
            List<TransactionRetiro> transactions = new List<TransactionRetiro>();
            var user = await _signInManager.UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                var error = new Error
                {
                    Desc_corta = "No se encontró usuario",
                };
                await InsertErrorAsync(error);
                return transactions;
            }

            transactions = await _dbContext.TransactionsRetiro.Where(t => t.Id_user == user.Id).ToListAsync();
            return transactions;
        }




        private async Task InsertErrorAsync(Error error)
        {
            await _dbContext.Errors.AddAsync(error);
            await _dbContext.SaveChangesAsync();
        }


    }
}

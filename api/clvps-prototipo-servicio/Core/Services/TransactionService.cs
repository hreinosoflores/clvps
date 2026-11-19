using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Entities;
using clvps_prototipo_servicio.Core.Interfaces;

namespace clvps_prototipo_servicio.Core.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionsListaItemResponse>> GetTransactionsByUser(string userName)
        {
            return await _transactionRepository.GetTransactionsByUser(userName);
        }

        public async Task<TransactionPagoResponse> GetTransactionPago(long id)
        {
            return await _transactionRepository.GetTransactionPago(id);
        }

        public async Task<TransactionRecargaResponse> GetTransactionRecarga(long id)
        {
            return await _transactionRepository.GetTransactionRecarga(id);
        }

        public async Task<TransactionRetiroResponse> GetTransactionRetiro(long id)
        {
            return await _transactionRepository.GetTransactionRetiro(id);
        }
    }
}

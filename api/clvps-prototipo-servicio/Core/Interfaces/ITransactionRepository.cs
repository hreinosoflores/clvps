using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Entities;

namespace clvps_prototipo_servicio.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TransactionsListaItemResponse>> GetTransactionsByUser(string userName);
        Task<TransactionPagoResponse> GetTransactionPago(long id);
        Task<TransactionRecargaResponse> GetTransactionRecarga(long id);
        Task<TransactionRetiroResponse> GetTransactionRetiro(long id);
    }
}

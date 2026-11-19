using clvps_prototipo_servicio.Core.Entities;

namespace clvps_prototipo_servicio.Api.DTO
{
    public class TransactionsListaItemResponse
    {
        public long? Id { get; set; }
        public double? Monto { get; set; }
        public string? Fecha { get; set; }
        public string? Hora { get; set; }

        //Ordenes de Pago
        public long? Id_ord { get; set; }
        public string? Tipo { get; set; }

        //Recargas
        public long? Id_rec { get; set; }

        //Retiros
        public long? Id_retiro { get; set; }
    }
}

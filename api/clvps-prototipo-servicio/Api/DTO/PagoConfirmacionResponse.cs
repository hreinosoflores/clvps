namespace clvps_prototipo_servicio.Api.DTO
{
    public class PagoConfirmacionResponse
    {
        public long? IdOrden { get; set; }
        public string? Beneficiario { get; set; }
        public double? MontoPagado { get; set; }
        public string? FechaTransaccion { get; set; }
        public string? HoraTransaccion { get; set; }
        public string? EmailBuyer { get; set; }
        public required bool Ok { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

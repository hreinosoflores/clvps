namespace clvps_prototipo_servicio.Api.DTO
{
    public class PagoDetalleResponse
    {
        public string? Beneficiario { get; set; }
        public long? IdOrden { get; set; }
        public string? NroCuenta { get; set; }
        public double? SaldoCuenta { get; set; }
        public double? MontoPagar { get; set; }
        public required bool Ok { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

namespace clvps_prototipo_servicio.Api.DTO
{
    public class DatosBasicosResponse
    {
        public string? ID_CLVPS { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }        
        public string? Nro_cuenta { get; set; }
        public double? Saldo_soles { get; set; }
        public double? Saldo_dolares { get; set; }
        public required bool Ok { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

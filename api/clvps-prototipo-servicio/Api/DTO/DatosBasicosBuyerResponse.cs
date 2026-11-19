namespace clvps_prototipo_servicio.Api.DTO
{
    public class DatosBasicosBuyerResponse : DatosBasicosResponse
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Nro_dni { get; set; }
        public string? Nro_carext { get; set; }
    }
}

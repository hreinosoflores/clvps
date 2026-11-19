namespace clvps_prototipo_servicio.Core.Entities
{
    public class SystemManager
    {
        public long Id { get; set; }
        public required string Pais { get; set; }
        public required string Ciudad { get; set; }
        public required string Distrito { get; set; }
        public string? DomicilioFiscal { get; set; }
        public required double SMFEE { get; set; }
    }
}


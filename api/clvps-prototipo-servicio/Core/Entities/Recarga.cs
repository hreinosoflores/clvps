namespace clvps_prototipo_servicio.Core.Entities
{
    public class Recarga
    {
        public long Id { get; set; }
        public required double Monto { get; set; }
        public required double Ges { get; set; }
        public required double Gsm { get; set; }
        public required double Mesra { get; set; }
        public required double Msmra { get; set; }
        public required long Id_es { get; set; }
        public virtual ExchangeSpot? ExchangeSpot { get; set; }
        public required long Id_sm { get; set; }       
        public virtual SystemManager? SystemManager { get; set; }
    }
}


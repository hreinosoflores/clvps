namespace clvps_prototipo_servicio.Core.Entities
{
    public class OrdenPago
    {
        public long Id { get; set; }
        public required double Monto { get; set; }
        public required string Estado { get; set; }
        public string Fecha { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string Hora { get; set; } = DateTime.Now.ToString("HH:mm:ss");
        public string? Id_b { get; set; }
        public virtual Buyer? Buyer { get; set; }
        public required string Id_s { get; set; }
        public virtual Seller? Seller { get; set; }
    }

}

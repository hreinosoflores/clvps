namespace clvps_prototipo_servicio.Api.DTO
{
    public class LoginResponse
    {
        public string? UserName { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Rol { get; set; }
        public required bool Ok { get; set; }
        public string? ErrorMessage { get; set; }

    }
}

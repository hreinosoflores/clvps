using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Interfaces;

namespace clvps_prototipo_servicio.Core.Services
{
    public class DatosBasicosService
    {
        private readonly IDatosBasicosRepository _datosBasicosRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public DatosBasicosService(IDatosBasicosRepository datosBasicosRepository, IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _datosBasicosRepository = datosBasicosRepository;
        }

        public async Task<DatosBasicosResponse> GetDatosBasicos(string userName)
        {
            //obtener rol de usuario
            string rol = await _usuarioRepository.GetRol(userName);

            switch (rol)
            {
                case "Buyer":
                    return await _datosBasicosRepository.GetDatosBasicosBuyer(userName);
                case "Seller":
                    return await _datosBasicosRepository.GetDatosBasicosSeller(userName);
                default:
                    return new DatosBasicosResponse() { Ok = false, ErrorMessage = rol };
            }      
        }
    }
}

using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.DTO;

namespace LPPA_Colaiacovo_Entidades.Mappers
{
    public static class UsuarioDTOMapper
    {
        public static UsuarioDTO UsuarioToUsuarioDTO(Usuario usuario)
        {
            return new UsuarioDTO()
            {
                Id = usuario.Id,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                FechaNacimiento = usuario.FechaNacimiento,
                Nombre = usuario.Nombre,
                Rol = usuario.Rol,
            };
        }
    }
}

using System;

namespace LPPA_Colaiacovo_Entidades.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Rol { get; set; }
    }
}

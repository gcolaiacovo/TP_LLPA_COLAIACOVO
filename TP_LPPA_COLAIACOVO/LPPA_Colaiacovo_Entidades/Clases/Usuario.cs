using System;

namespace LPPA_Colaiacovo_Entidades.Clases
{
    [Serializable]
    public class Usuario
    {
        public Usuario()
        {
                
        }

        public Usuario(int id, string nombre, string apellido, string email, string contrasena, DateTime? fechaNacimiento, string rol, bool activo, DateTime fechaCreado, DateTime? fechaModificado, int digitoVerificador)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Contrasena = contrasena;
            FechaNacimiento = fechaNacimiento;
            Rol = rol;
            Activo = activo;
            FechaCreado = fechaCreado;
            FechaModificado = fechaModificado;
            DigitoVerificador = digitoVerificador;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public DateTime? FechaModificado { get; set; }
        public int DigitoVerificador { get; set; }

        public string GetName()
        {
            return Nombre + " " + Apellido;
        }
    }
}

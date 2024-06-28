using LPPA_Colaiacovo_DAL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.Excepciones;
using LPPA_Colaiacovo_Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPPA_Colaiacovo_BLL.Clases
{
    public class BLLUsuario
    {
        private readonly DALUsuario DALUsuario;

        public BLLUsuario()
        {
            DALUsuario = new DALUsuario();
        }

        public bool CambiarContraseña(int id, string curPassword, string newPass)
        {
            var usuario = GetUsuario(id);

            if (usuario != null &&
                EncryptionService.Encriptar(curPassword) == usuario.Contrasena)
            {
                usuario.Contrasena = EncryptionService.Encriptar(newPass);

                DALUsuario.Save(usuario);

                return true;
            }

            return false;
        }

        public Usuario GetUsuario(int id)
        {
            var usuario = DALUsuario.Get(id);
            var digitoVerificador = CalcularChecksum(usuario);
            if (digitoVerificador != usuario.DigitoVerificador)
            {
                throw new DigitoVerificadorException(usuario.Id);
            }

            return usuario;
        }

        public List<Usuario> GetUsuarios()
        {
            var usuarios = DALUsuario.GetAll();

            foreach (var usuario in usuarios)
            {
                var digitoVerificador = CalcularChecksum(usuario);
                if (digitoVerificador != usuario.DigitoVerificador)
                {
                    throw new DigitoVerificadorException(usuario.Id);
                }
            }

            return usuarios;
        }

        public void Save(Usuario usuario)
        {
            usuario.DigitoVerificador = CalcularChecksum(usuario);
            DALUsuario.Save(usuario);
        }

        public byte CalcularChecksum(Usuario entidad)
        {
            // Concatenar las propiedades de la entidad en una cadena
            StringBuilder sb = new StringBuilder();
            sb.Append(entidad.Nombre);
            sb.Append(entidad.Apellido);
            sb.Append(entidad.Email);
            sb.Append(entidad.Contrasena);
            if (entidad.FechaNacimiento.HasValue)
                sb.Append(entidad.FechaNacimiento.Value.ToString("yyyyMMdd"));
            sb.Append(entidad.Rol);
            sb.Append(entidad.Activo ? "1" : "0");
            sb.Append(entidad.FechaCreado.ToString("yyyyMMdd"));
            if (entidad.FechaModificado.HasValue)
                sb.Append(entidad.FechaModificado.Value.ToString("yyyyMMdd"));

            string concatenatedString = sb.ToString();

            // Convertir la cadena a un array de bytes (usando valores ASCII)
            byte[] data = Encoding.ASCII.GetBytes(concatenatedString);

            // Calcular el checksum como el complemento a 8 bits de la suma de los bytes
            int sum = data.Sum(b => b);
            byte checksum = (byte)~sum;

            return checksum;
        }

        public bool VerificarChecksum(Usuario entidad, byte checksum)
        {
            // Calcular el checksum de nuevo
            var calculatedChecksum = CalcularChecksum(entidad);

            // Verificar si el checksum calculado coincide con el proporcionado
            return calculatedChecksum == checksum;
        }
    }
}

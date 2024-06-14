using LPPA_Colaiacovo_DAL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Services;
using System.Collections.Generic;

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
            return DALUsuario.Get(id);
        }

        public List<Usuario> GetUsuarios()
        {
            return DALUsuario.GetAll();
        }

        public void Save(Usuario usuario)
        {
            DALUsuario.Save(usuario);
        }
    }
}

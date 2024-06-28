using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.Mappers;
using LPPA_Colaiacovo_Mapper;
using LPPA_Colaiacovo_Services;
using Newtonsoft.Json;
using System;
using System.Web;

public partial class Login : System.Web.UI.Page
{
    private readonly BLLBitacora bLLBitacora;
    private readonly BLLUsuario bLLUsuario;

    public Login()
    {
        bLLBitacora = new BLLBitacora();
        bLLUsuario = new BLLUsuario();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        var usuarios = bLLUsuario.GetUsuarios();

        string email = txtUsuario.Text;
        string contrasena = txtContrasena.Text;

        var usuario = usuarios.Find(x => x.Email == email && x.Contrasena == EncryptionService.Encriptar(contrasena));
        if (usuario == null)
        {
            lblMensaje.Text = "Usuario o contraseña incorrectos";
        }
        else
        {
            var usuarioDTO = UsuarioDTOMapper.UsuarioToUsuarioDTO(usuario);
            HttpCookie cookie = new HttpCookie("UsuarioLogueado");
            cookie.Value = JsonConvert.SerializeObject(usuarioDTO);
            cookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(cookie);

            bLLBitacora.SaveBitacora(new Bitacora()
            {
                IdUsuario = usuarioDTO.Id,
                Descripcion = Constantes.USER_LOGGED_IN,
            });

            var redireccion = usuarioDTO.Rol == Constantes.ROL_ADMIN ? "Administracion.aspx" : "Default.aspx";
            Response.Redirect(redireccion);
        }
    }
}
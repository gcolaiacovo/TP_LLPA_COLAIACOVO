using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.DTO;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.UI;

public partial class CambiarContraseña : System.Web.UI.UserControl
{
    private readonly BLLUsuario bLLUsuario;

    public CambiarContraseña()
    {
        bLLUsuario = new BLLUsuario();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCambiarContraseña_Click(object sender, EventArgs e)
    {
        string curPassword = currentPassword.Text;
        string newPass = newPassword.Text;
        string confirmPass = confirmNewPassword.Text;

        // Aquí puedes agregar la lógica para cambiar la contraseña
        // Por ejemplo:
        if (newPass == confirmPass)
        {
            string script = "alert('Contraseña cambiada exitosamente.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", script, true);

            HttpCookie cookie = HttpContext.Current.Request.Cookies["UsuarioLogueado"];
            if (cookie != null)
            {
                string cookieValue = cookie.Value;

                // Deserializar el valor de la cookie a un objeto UsuarioDTO
                UsuarioDTO usuarioLogueado = JsonConvert.DeserializeObject<UsuarioDTO>(cookieValue);

                var exito = bLLUsuario.CambiarContraseña(usuarioLogueado.Id, curPassword, newPass);
            }
        }
        else
        {
            string script = "alert('Las contraseñas no coinciden');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessage", script, true);
        }
    }
}
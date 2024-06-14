using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Mapper;
using LPPA_Colaiacovo_Services;
using System;

public partial class Registro : System.Web.UI.Page
{
    private readonly BLLUsuario bLLUsuario;
    private readonly BLLBitacora bLLBitacora;

    public Registro()
    {
        bLLUsuario = new BLLUsuario();
        bLLBitacora = new BLLBitacora();
    }

    protected void btnRegistro_Click(object sender, EventArgs e)
    {
        var usuario = new Usuario();
        usuario.Nombre = txtFirstName.Text;
        usuario.Apellido = txtLastName.Text;
        usuario.Contrasena = EncryptionService.Encriptar(txtFirstName.Text);
        usuario.Email = txtEmail.Text;
        usuario.Rol = Constantes.ROL_USUARIO;
        usuario.FechaNacimiento = DateTime.Parse(txtBirthDate.Text);

        bLLUsuario.Save(usuario);

        bLLBitacora.SaveBitacora(new Bitacora()
        {
            Descripcion = "Se ha creado un nuevo usuario con ID " + usuario.Id,
            IdUsuario = usuario.Id,
        });

        Response.Redirect("Login.aspx?showAlert=true");
    }
}
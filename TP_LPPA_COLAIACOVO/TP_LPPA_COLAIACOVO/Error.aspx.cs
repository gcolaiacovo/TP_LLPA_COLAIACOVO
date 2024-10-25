using LPPA_Colaiacovo_Mapper;
using LPPA_Colaiacovo_Services;
using Newtonsoft.Json;
using System;
using System.Web;

public partial class Error : System.Web.UI.Page
{
    public bool IsAdmin { get; private set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var cookie = Request.Cookies["UsuarioLogueado"];
            if (cookie != null)
            {
                try
                {
                    var usuario = JsonConvert.DeserializeObject<LPPA_Colaiacovo_Entidades.DTO.UsuarioDTO>(cookie.Value);
                    if (usuario != null && usuario.Rol == Constantes.ROL_ADMIN)
                    {
                        IsAdmin = true;
                    }
                }
                catch (Exception ex)
                {
                }
            }

            var msjError = Request.QueryString["mensajeError"];
            if (!string.IsNullOrEmpty(msjError))
            {
                msjError = HttpUtility.HtmlDecode(msjError);
                msjError = msjError.Replace("\n", "<br/>");
                mensajeError.Text = msjError;
            }
        }
    }

    protected void btnRestaurar_Click(object sender, EventArgs e)
    {
        DatabaseBackupService databaseBackupService = new DatabaseBackupService();
        try
        {
            databaseBackupService.CargarBackupBaseDeDatos();
            Response.Redirect("Default.aspx", false);
        }
        catch (Exception ex) { }
        {
        }
    }
}
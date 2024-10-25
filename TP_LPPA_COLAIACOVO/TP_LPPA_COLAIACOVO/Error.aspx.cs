using LPPA_Colaiacovo_Services;
using System;
using System.Web;

public partial class Error : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
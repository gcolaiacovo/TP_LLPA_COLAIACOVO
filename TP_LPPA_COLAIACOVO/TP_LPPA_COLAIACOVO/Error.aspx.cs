using System;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["mensajeError"] != null)
            {
                var codigoError = Server.UrlDecode(Request.QueryString["mensajeError"]);
                // Hacer algo con el mensaje de error, por ejemplo, mostrarlo en una etiqueta
                mensajeError.Text = codigoError;
            }
        }
    }
}
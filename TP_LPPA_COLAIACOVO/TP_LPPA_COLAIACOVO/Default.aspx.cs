using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.Excepciones;
using LPPA_Colaiacovo_Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

public partial class _Default : System.Web.UI.Page
{
    private DatabaseBackupService Dbs;
    private readonly BLLUsuario BLLUsuario;
    private readonly BLLProducto BLLProducto;
    private List<Usuario> usuarios = null;
    private List<Producto> productos = null;

    public _Default()
    {
        BLLUsuario = new BLLUsuario();
        BLLProducto = new BLLProducto();
        Dbs = new DatabaseBackupService();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            usuarios = BLLUsuario.GetUsuarios();

            HttpCookie cookie = new HttpCookie("Usuarios");
            cookie.Value = JsonConvert.SerializeObject(usuarios);
            Response.Cookies.Add(cookie);

            productos = BLLProducto.GetProductos();

            cookie = new HttpCookie("Productos");
            cookie.Value = JsonConvert.SerializeObject(productos);
            Response.Cookies.Add(cookie);
        }
        catch (DigitoVerificadorException ex)
        {
            var ids = ex.Ids;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Error en digitos verificadores: ");
            ids.ForEach(id => { sb.AppendLine(ex.EntidadTipo + " con ID " + id); });
            string mensajeFormateado = sb.ToString().Replace(Environment.NewLine, "<br/>");
            string mensajeCodificado = HttpUtility.HtmlEncode(mensajeFormateado);
            Response.Redirect("Error.aspx?mensajeError=" + Server.UrlEncode(mensajeCodificado));
        }
        catch (Exception ex)
        {
            var mensajeError = ex.Message;
            Response.Redirect("Error.aspx?mensajeError=" + Server.UrlEncode(mensajeError));
        }
    }
}
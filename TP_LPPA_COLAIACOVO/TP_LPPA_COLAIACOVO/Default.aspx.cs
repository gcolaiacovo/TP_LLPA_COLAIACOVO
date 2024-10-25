using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_DAL.Clases;
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
    private readonly BLLUsuario bLLUsuario;
    private readonly BLLProducto bLLProducto;
    private List<Usuario> usuarios = null;
    private List<Producto> productos = null;

    public _Default()
    {
        bLLUsuario = new BLLUsuario();
        bLLProducto = new BLLProducto();
        Dbs = new DatabaseBackupService();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie;
        StringBuilder sb = new StringBuilder();
        try
        {
            usuarios = bLLUsuario.GetUsuarios();
            cookie = new HttpCookie("Usuarios");
            cookie.Value = JsonConvert.SerializeObject(usuarios);
            Response.Cookies.Set(cookie);
        }
        catch (DigitoVerificadorException ex)
        {
            var ids = ex.Ids;
            sb.AppendLine("Error en digitos verificadores: ");
            ids.ForEach(id => { sb.AppendLine(ex.EntidadTipo + " con ID " + id); });
        }

        try
        {
            productos = bLLProducto.GetProductos();
            cookie = new HttpCookie("Productos");
            cookie.Value = JsonConvert.SerializeObject(productos);
            Response.Cookies.Set(cookie);
        }
        catch (DigitoVerificadorException ex)
        {
            var ids = ex.Ids;
            if (sb.Length == 0)
            {
                sb.AppendLine("Error en digitos verificadores: ");
            }
            ids.ForEach(id => { sb.AppendLine(ex.EntidadTipo + " con ID " + id); });
        }

        if (sb.Length > 0)
        {
            string mensajeFormateado = sb.ToString().Replace(Environment.NewLine, "<br/>");
            string mensajeCodificado = HttpUtility.HtmlEncode(mensajeFormateado);
            Response.Redirect("Error.aspx?mensajeError=" + Server.UrlEncode(mensajeCodificado));
        }
    }
}
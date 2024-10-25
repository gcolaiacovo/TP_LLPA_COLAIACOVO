using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.Enums;
using LPPA_Colaiacovo_Entidades.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public partial class Productos : System.Web.UI.UserControl
{
    private readonly BLLUsuario bLLUsuario;
    private readonly BLLProducto bLLProducto;

    public Productos()
    {
        bLLUsuario = new BLLUsuario();
        bLLProducto = new BLLProducto();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    var usuarios = bLLUsuario.GetUsuarios();
                }
                catch (DigitoVerificadorException ex)
                {
                    var ids = ex.Ids;
                    sb.AppendLine("Error en digitos verificadores: ");
                    ids.ForEach(id => { sb.AppendLine(ex.EntidadTipo + " con ID " + id); });
                }

                var productos = new List<Producto>();
                try
                {
                    productos = bLLProducto.GetProductos();
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

                if (productos.Any())
                {
                    repeaterProductos.DataSource = productos.Where(o => o.Activo && o.CategoriaId == CategoriaProductoEnum.Producto).ToList();
                    repeaterProductos.DataBind();

                    repeaterServicios.DataSource = productos.Where(o => o.Activo && o.CategoriaId == CategoriaProductoEnum.Servicio).ToList();
                    repeaterServicios.DataBind();
                }
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
}
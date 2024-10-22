using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Mapper;
using LPPA_Colaiacovo_Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LPPA_WebService;
using Usuario = LPPA_Colaiacovo_Entidades.Clases.Usuario;
using Producto = LPPA_Colaiacovo_Entidades.Clases.Producto;
using Venta = LPPA_Colaiacovo_Entidades.Clases.Venta;


public partial class Administracion : System.Web.UI.Page
{
    private readonly BLLBitacora bLLBitacora;
    private readonly BLLUsuario bLLUsuario;
    private readonly BLLProducto bLLProducto;
    private readonly BLLVenta bLLVenta;
    private List<Usuario> usuarios = null;
    private List<Producto> productos = null;
    private List<Bitacora> bitacoras = null;
    private List<Venta> ventas = null;
    private DatabaseBackupService databaseBackupService;

    public Administracion()
    {
        bLLBitacora = new BLLBitacora();
        bLLUsuario = new BLLUsuario();
        bLLProducto = new BLLProducto();
        bLLVenta = new BLLVenta();
        databaseBackupService = new DatabaseBackupService();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var usuarioLogueado = Request.Cookies["UsuarioLogueado"];

        if (usuarioLogueado == null ||
           (JsonConvert.DeserializeObject<Usuario>(usuarioLogueado.Value).Rol != Constantes.ROL_ADMIN))
        {
            var mensajeError = "No tiene permisos suficientes para acceder a esta área!";
            Response.Redirect("Error.aspx?mensajeError=" + Server.UrlEncode(mensajeError));
        }

        try
        {
            usuarios = bLLUsuario.GetUsuarios();
            HttpCookie cookie = new HttpCookie("Usuarios");
            cookie.Value = JsonConvert.SerializeObject(usuarios);
            Response.Cookies.Set(cookie);

            productos = bLLProducto.GetProductos();
            cookie = new HttpCookie("Productos");
            cookie.Value = JsonConvert.SerializeObject(productos);
            Response.Cookies.Set(cookie);

            bitacoras = bLLBitacora.GetBitacoras().OrderByDescending(p => p.FechaCreado).ToList();
            cookie = new HttpCookie("Bitacoras");
            cookie.Value = JsonConvert.SerializeObject(bitacoras);
            Response.Cookies.Set(cookie);

            ventas = bLLVenta.GetAll().OrderByDescending(p => p.FechaCreado).ToList();

            ventas.ForEach(v =>
            {
                v.VentaProductos.ForEach(vp =>
                {
                    vp.Producto = productos.FirstOrDefault(p => p.Id == vp.IdProducto);
                });
                v.Usuario = usuarios.FirstOrDefault(u => u.Id == v.IdUsuario);
            });

            cookie = new HttpCookie("Ventas");
            cookie.Value = JsonConvert.SerializeObject(ventas);
            Response.Cookies.Set(cookie);

            foreach (var bitacora in bitacoras)
            {
                if (!bitacora.IdUsuario.HasValue || bitacora.IdUsuario == 0)
                {
                    bitacora.Usuario = new Usuario();
                    continue;
                }

                var usuario = usuarios.FirstOrDefault(o => o.Id == bitacora.IdUsuario.Value);
                bitacora.Usuario = usuario;
            }
        }
        catch (Exception ex)
        {
            var mensajeError = ex.Message;
            Response.Redirect("Error.aspx?mensajeError=" + Server.UrlEncode(mensajeError));
        }

        ViewState["Bitacoras"] = bitacoras;
        ViewState["Productos"] = productos;
        ViewState["Usuarios"] = usuarios;
        ViewState["Ventas"] = ventas;
    }

    protected void crearBackup_Click(object sender, EventArgs e)
    {
        try
        {
            databaseBackupService.CrearBackupBaseDeDatos();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El backup de la base de datos se creó correctamente.');", true);
        }
        catch (Exception ex)
        {
            bLLBitacora.SaveBitacora(new Bitacora()
            {
                Descripcion = "Error al crear el backup de la base de datos! " + ex.Message,
            });

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al crear el backup de la base de datos!');", true);
        }
    }

    protected void recuperarBackup_Click(object sender, EventArgs e)
    {
        try
        {
            HttpCookie cookie = new HttpCookie("UsuarioLogueado");
            cookie.Expires = DateTime.Now.AddDays(-1);

            Response.Cookies.Add(cookie);

            databaseBackupService.CargarBackupBaseDeDatos();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('El backup de la base de datos se recuperó correctamente. Por favor inicie sesión nuevamente'); window.href='/Login.aspx'", true);
        }
        catch (Exception ex)
        {
            bLLBitacora.SaveBitacora(new Bitacora()
            {
                Descripcion = "Error al recuperar el backup de la base de datos! " + ex.Message,
            });

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al recuperar el backup de la base de datos!');", true);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        var ventasSeleccionadas = new List<int>();
        foreach (string key in Request.Form.AllKeys)
        {
            if (key.StartsWith("ventaSeleccionada"))
            {
                string valorSeleccionado = Request.Form[key];

                string[] idsArray = valorSeleccionado.Trim('[', ']').Split(',');

                foreach (string id in idsArray)
                {
                    var intId = Convert.ToInt32(id.Trim('\"'));
                    ventasSeleccionadas.Add(intId);
                }
            }
        }

        // si selecciono alguna venta
        if (ventasSeleccionadas.Any())
        {
            var webService = new MiWebServiceSoapClient();
            var p = new ArrayOfInt();
            p.AddRange(ventasSeleccionadas);
            var xmlStream = webService.GenerarXMLVentas(p);
            DescargarArchivoXML(xmlStream, "VentasExportadas.xml");
        }
        // si no selecciono venta
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Debe seleccionar alguna venta para exportar!');", true);
        }
    }

    private List<Venta> FiltrarVentasDesdeCookie(HttpCookie cookie, List<int> ventasSeleccionadas)
    {
        List<Venta> todasLasVentas = ObtenerVentasDesdeCookie(cookie);
        List<Venta> ventasFiltradas = todasLasVentas.FindAll(v => ventasSeleccionadas.Contains(v.Id));
        return ventasFiltradas;
    }

    private List<Venta> ObtenerVentasDesdeCookie(HttpCookie cookie)
    {
        if (cookie != null && cookie.Value != null)
        {
            var ventas = JsonConvert.DeserializeObject<List<Venta>>(cookie.Value);
            return ventas;
        }

        return new List<Venta>();
    }

    private void DescargarArchivoXML(string xmlContent, string nombreArchivo)
    {
        Response.Clear();
        Response.ContentType = "application/xml";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);

        // Convertir el string a bytes
        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xmlContent);

        // Escribir el arreglo de bytes en el Response
        Response.OutputStream.Write(byteArray, 0, byteArray.Length);

        Response.Flush();
        Response.End();
    }
}
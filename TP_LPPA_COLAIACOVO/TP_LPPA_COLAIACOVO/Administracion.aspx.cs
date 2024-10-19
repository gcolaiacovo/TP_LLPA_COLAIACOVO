using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Mapper;
using LPPA_Colaiacovo_Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class Administracion : System.Web.UI.Page
{
    private readonly BLLBitacora bLLBitacora;
    private readonly BLLUsuario bLLUsuario;
    private readonly BLLProducto bLLProducto;
    private List<Usuario> usuarios = null;
    private List<Producto> productos = null;
    private List<Bitacora> bitacoras = null;
    private DatabaseBackupService databaseBackupService;

    public Administracion()
    {
        bLLBitacora = new BLLBitacora();
        bLLUsuario = new BLLUsuario();
        bLLProducto = new BLLProducto();
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
            Response.Cookies.Add(cookie);
            
            productos = bLLProducto.GetProductos();
            cookie = new HttpCookie("Productos");
            cookie.Value = JsonConvert.SerializeObject(productos);
            Response.Cookies.Add(cookie);
            
            bitacoras = bLLBitacora.GetBitacoras().OrderByDescending(p => p.FechaCreado).ToList();
            cookie = new HttpCookie("Bitacoras");
            cookie.Value = JsonConvert.SerializeObject(bitacoras);
            Response.Cookies.Add(cookie);

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
}
using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Mapper;
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

    public Administracion()
    {
        bLLBitacora = new BLLBitacora();
        bLLUsuario = new BLLUsuario();
        bLLProducto = new BLLProducto();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var usuarioLogueado = Request.Cookies["UsuarioLogueado"];

        if (usuarioLogueado == null ||
           (JsonConvert.DeserializeObject<Usuario>(usuarioLogueado.Value).Rol != Constantes.ROL_ADMIN))
        {
            Response.Redirect("Error.aspx");
        }

        if (!IsPostBack)
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
                if (!bitacora.IdUsuario.HasValue)
                {
                    continue;
                }

                var usuario = usuarios.FirstOrDefault(o => o.Id == bitacora.IdUsuario.Value);
                bitacora.Usuario = usuario;
            }
        }

        ViewState["Bitacoras"] = bitacoras;
        ViewState["Productos"] = productos;
        ViewState["Usuarios"] = usuarios;
    }
}
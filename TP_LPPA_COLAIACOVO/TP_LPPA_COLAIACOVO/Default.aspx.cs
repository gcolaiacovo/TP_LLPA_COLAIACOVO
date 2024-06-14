using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;

public partial class _Default : System.Web.UI.Page
{
    private readonly BLLUsuario BLLUsuario;
    private readonly BLLProducto BLLProducto;
    private List<Usuario> usuarios = null;
    private List<Producto> productos = null;

    public _Default()
    {
        BLLUsuario = new BLLUsuario();
        BLLProducto = new BLLProducto();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["Usuarios"] == null)
            {
                usuarios = BLLUsuario.GetUsuarios();

                HttpCookie cookie = new HttpCookie("Usuarios");
                cookie.Value = JsonConvert.SerializeObject(usuarios);
                Response.Cookies.Add(cookie);
            }
            if (Request.Cookies["Productos"] == null)
            {
                productos = BLLProducto.GetProductos();

                HttpCookie cookie = new HttpCookie("Productos");
                cookie.Value = JsonConvert.SerializeObject(productos);
                Response.Cookies.Add(cookie);
            }
        }
    }
}
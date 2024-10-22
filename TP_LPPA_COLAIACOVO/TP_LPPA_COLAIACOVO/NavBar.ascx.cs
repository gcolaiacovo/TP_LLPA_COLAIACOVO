using LPPA_Colaiacovo_Mapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class NavBar : System.Web.UI.UserControl
{
    public bool IsAdmin { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ActualizarItemsCarrito(ObtenerCantidadProductosEnCarrito());

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
                    // Maneja el error de deserialización
                }
            }
        }
    }

    protected void btnServerSide_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("UsuarioLogueado");
        cookie.Expires = DateTime.Now.AddDays(-1);

        Response.Cookies.Add(cookie);

        Response.Redirect("Login.aspx");
    }

    public void ActualizarItemsCarrito(int cantidad)
    {
        itemsCarrito.Text = cantidad.ToString();
    }

    private int ObtenerCantidadProductosEnCarrito()
    {
        var productosCookie = HttpContext.Current.Request.Cookies["ItemsEnCarrito"];

        if (productosCookie != null)
        {
            var productos = JsonConvert.DeserializeObject<List<int>>(productosCookie.Value);
            return productos.Distinct().Count();
        }
        return 0;
    }
}
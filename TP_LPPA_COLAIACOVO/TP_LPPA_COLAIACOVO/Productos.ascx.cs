using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class Productos : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie productosCookie = Request.Cookies["Productos"];

            if (productosCookie != null)
            {
                // Deserializar la cookie a una lista de productos
                string productosJson = productosCookie.Value;
                List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(productosJson);
                
                repeaterProductos.DataSource = productos.Where(o => o.Activo && o.CategoriaId == CategoriaProductoEnum.Producto).ToList();
                repeaterProductos.DataBind();

                repeaterServicios.DataSource = productos.Where(o => o.Activo && o.CategoriaId == CategoriaProductoEnum.Servicio).ToList();
                repeaterServicios.DataBind();
            }
        }
    }
}
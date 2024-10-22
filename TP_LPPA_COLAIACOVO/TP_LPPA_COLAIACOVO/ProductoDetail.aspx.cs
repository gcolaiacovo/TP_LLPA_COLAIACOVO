using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class ProductoDetail : System.Web.UI.Page
{
    private readonly BLLProducto bLLProducto;
    public ProductoDetail()
    {
        bLLProducto = new BLLProducto();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int productoId;
            if (int.TryParse(Request.QueryString["IdProducto"], out productoId))
            {
                var productos = bLLProducto.GetProductos();
                var producto = productos.SingleOrDefault(t => t.Id == productoId);

                if (producto != null)
                {
                    imgProducto.ImageUrl = producto.UrlImagen;
                    lblNombre.Text = producto.Nombre;
                    lblDescripcion.Text = producto.Descripcion;
                    lblMarca.Text = producto.Marca;
                    lblStock.Text = "En stock: " + producto.Stock.ToString();
                    lblPrecio.Text = "$ " + producto.Precio.ToString();

                    ViewState["Producto"] = producto;
                }
                else
                {
                    Response.Write("ID de producto no válido.");
                }
            }
            else
            {
                // Manejar el caso en que no se proporcione un ID válido
                Response.Write("ID de producto no válido.");
            }
        }
    }

    protected void btnAgregarCarrito_Click(object sender, EventArgs e)
    {
        var productosCookie = HttpContext.Current.Request.Cookies["ItemsEnCarrito"];
        List<int> productosEnCarrito = new List<int>();

        if (productosCookie != null)
        {
            productosEnCarrito = JsonConvert.DeserializeObject<List<int>>(productosCookie.Value);
        }

        var cant = Convert.ToInt32(txtCantidad.Text);
        for (int i = 0; i < cant; i++)
        {
            productosEnCarrito.Add(((Producto)ViewState["Producto"]).Id);
        }

        // Guardar la lista actualizada en la cookie
        var cookie = new HttpCookie("ItemsEnCarrito");
        cookie.Value = JsonConvert.SerializeObject(productosEnCarrito);
        Response.Cookies.Set(cookie);

        ClientScript.RegisterStartupScript(this.GetType(), "UpdateItemCount", "actualizarItemsCarrito();", true);
    }
}
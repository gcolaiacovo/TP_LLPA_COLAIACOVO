using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class FinalizarCompra : System.Web.UI.Page
{
    private readonly BLLProducto bLLProducto;

    public FinalizarCompra()
    {
        bLLProducto = new BLLProducto();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var itemsEnCarritoCookie = HttpContext.Current.Request.Cookies["ItemsEnCarrito"];
        List<int> productoIdsEnCarrito = new List<int>();

        if (itemsEnCarritoCookie != null)
        {
            productoIdsEnCarrito = JsonConvert.DeserializeObject<List<int>>(itemsEnCarritoCookie.Value);
        }

        var productos = bLLProducto.GetProductos();

        if (productos.Any())
        {
            var productosEnCarrito = productoIdsEnCarrito
                .GroupBy(p => p)
                .Select(group => new
                {
                    Producto = productos.FirstOrDefault(t => t.Id == group.Key),
                    Cantidad = group.Count()
                })
                .Select(g => new ProductoCarritoDTO
                {
                    Nombre = g.Producto.Nombre,
                    Descripcion = g.Producto.Descripcion,
                    Marca = g.Producto.Marca,
                    CategoriaId = g.Producto.CategoriaId,
                    Precio = g.Producto.Precio,
                    UrlImagen = g.Producto.UrlImagen,
                    Cantidad = g.Cantidad,
                    PrecioTotal = g.Producto.Precio * g.Cantidad
                })
                .ToList();

            ViewState["ProductosCarrito"] = productosEnCarrito;
        }
    }
}
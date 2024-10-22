using LPPA_Colaiacovo_BLL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.DTO;
using LPPA_Colaiacovo_Entidades.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public partial class FinalizarCompra : System.Web.UI.Page
{
    private readonly BLLProducto bLLProducto;
    private readonly BLLVenta bLLVenta;

    public FinalizarCompra()
    {
        bLLProducto = new BLLProducto();
        bLLVenta = new BLLVenta();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ActualizarTablaCarrito();
        if (!IsPostBack)
        {
            transferDetails.Visible = false;
            cashDetails.Visible = false;
            cardDetails.Visible = false;
            var productosEnCarrito = GetProductosEnCarrito();
            if (productosEnCarrito.Count == 0)
            {
                formPago.Visible = false;
            }
        }
    }

    private void ActualizarTablaCarrito()
    {
        var productosEnCarrito = GetProductosEnCarrito();
        ViewState["ProductosCarrito"] = productosEnCarrito;

        productosCarritoPlaceholder.Controls.Clear();
        foreach (var prod in productosEnCarrito)
        {
            TableRow row = new TableRow();

            row.Cells.Add(new TableCell { Text = prod.Nombre });
            row.Cells.Add(new TableCell { Text = prod.Descripcion });
            row.Cells.Add(new TableCell { Text = prod.Marca });
            row.Cells.Add(new TableCell { Text = prod.Cantidad.ToString() });
            row.Cells.Add(new TableCell { Text = "$ " + prod.PrecioTotal.ToString() });

            // Crear botón de eliminar
            TableCell cell = new TableCell();
            ImageButton btnEliminar = new ImageButton();
            btnEliminar.ID = "btnEliminar_" + prod.Id;
            btnEliminar.CommandArgument = prod.Id.ToString();
            btnEliminar.Click += btnEliminar_Click;
            btnEliminar.ImageUrl = "~/Resources/bin.png";
            btnEliminar.Width = 16;

            cell.Controls.Add(btnEliminar);
            row.Cells.Add(cell);

            productosCarritoPlaceholder.Controls.Add(row);
        }

        if (productosEnCarrito.Count == 0)
        {
            formPago.Visible = false;
        }
    }

    private List<ProductoCarritoDTO> GetProductosEnCarrito()
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
                    Id = g.Producto.Id,
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

            return productosEnCarrito;
        }

        return null;
    }

    private string ValidarMedioDePago()
    {
        var mediodePago = (MetodoDePagoEnum)Convert.ToInt32(ddlPaymentMethod.SelectedValue);

        if (mediodePago == MetodoDePagoEnum.TarjetaCredito)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Error con los datos de la tarjeta:");
            if (string.IsNullOrEmpty(txtCardNumber.Text) ||
                string.IsNullOrEmpty(txtCardHolder.Text) ||
                string.IsNullOrEmpty(txtCardExpiry.Text) ||
                string.IsNullOrEmpty(txtCardCVV.Text))
            {
                if (string.IsNullOrEmpty(txtCardNumber.Text))
                {
                    sb.Append("- El número de la tarjeta está vacío");
                }
                if (string.IsNullOrEmpty(txtCardHolder.Text))
                {
                    sb.Append("- El nombre del dueño de la tarjeta está vacío");
                }
                if (string.IsNullOrEmpty(txtCardExpiry.Text))
                {
                    sb.Append("- La fecha de expiración de la tarjeta está vacío");
                }
                if (string.IsNullOrEmpty(txtCardCVV.Text))
                {
                    sb.Append("- El código de la tarjeta está vacío");
                }
            }

            return sb.ToString();
        }
        else if (mediodePago == MetodoDePagoEnum.Ninguno)
        {
            return "Seleccione un medio de pago";
        }

        return null;
    }

    protected void btnFinalizarCompra_Click(object sender, EventArgs e)
    {
        var validarMedioDePagoError = ValidarMedioDePago();
        if (!string.IsNullOrEmpty(validarMedioDePagoError))
        {
            var err = "alert('" + validarMedioDePagoError + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", err, true);
            return;
        }

        var productos = bLLProducto.GetProductos();

        var productosEnCarrito = GetProductosEnCarrito();

        var venta = new Venta();
        venta.VentaProductos = new List<VentaProducto>();
        venta.MetodoDePago = (MetodoDePagoEnum)Convert.ToInt32(ddlPaymentMethod.SelectedValue);
        productosEnCarrito.ForEach(producto =>
        {
            venta.VentaProductos.Add(new VentaProducto()
            {
                Cantidad = producto.Cantidad,
                IdProducto = producto.Id,
                Monto = producto.PrecioTotal,
                FechaCreado = DateTime.Now,
            });
        });

        var cookie = Request.Cookies["UsuarioLogueado"];
        if (cookie != null)
        {
            var usuario = JsonConvert.DeserializeObject<LPPA_Colaiacovo_Entidades.DTO.UsuarioDTO>(cookie.Value);
            if (usuario != null)
            {
                venta.IdUsuario = usuario.Id;
            }
        }

        venta.MontoTotal = Convert.ToDecimal(venta.VentaProductos.Sum(t => t.Monto));

        bLLVenta.Save(venta);
    }

    protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Ocultar todas las secciones inicialmente
        transferDetails.Visible = false;
        cashDetails.Visible = false;
        cardDetails.Visible = false;

        // Mostrar la sección según la selección del medio de pago
        switch ((MetodoDePagoEnum)Convert.ToInt32(ddlPaymentMethod.SelectedValue))
        {
            case MetodoDePagoEnum.Transferencia:
                transferDetails.Visible = true;
                break;
            case MetodoDePagoEnum.Efectivo:
                cashDetails.Visible = true;
                break;
            case MetodoDePagoEnum.TarjetaCredito:
                cardDetails.Visible = true;
                break;
            default:
                break;
        }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        ImageButton btnQuitarProd = (ImageButton)sender;
        int productoId = Convert.ToInt32(btnQuitarProd.CommandArgument);

        if (productoId != 0)
        {
            QuitarDelCarrito(productoId);
            ActualizarTablaCarrito();
            ClientScript.RegisterStartupScript(this.GetType(), "UpdateItemCount", "actualizarItemsCarrito();", true);
        }
    }

    private void QuitarDelCarrito(int productoId)
    {
        var productosCookie = HttpContext.Current.Request.Cookies["ItemsEnCarrito"];
        List<int> productosEnCarrito = new List<int>();

        if (productosCookie != null)
        {
            productosEnCarrito = JsonConvert.DeserializeObject<List<int>>(productosCookie.Value);
        }

        productosEnCarrito.RemoveAll(t => t == productoId);

        // Guardar la lista actualizada en la cookie
        var cookie = new HttpCookie("ItemsEnCarrito");
        cookie.Value = JsonConvert.SerializeObject(productosEnCarrito);
        Response.Cookies.Set(cookie);
        Request.Cookies.Set(cookie);
    }
}
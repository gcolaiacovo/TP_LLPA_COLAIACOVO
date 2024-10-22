using LPPA_Colaiacovo_BLL.Clases;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Xml;

/// <summary>
/// Descripción breve de WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class MiWebService : System.Web.Services.WebService
{
    private readonly BLLUsuario bLLUsuario;
    private readonly BLLVenta bLLVenta;
    private readonly BLLProducto  bLLProducto;
    public MiWebService()
    {
        bLLUsuario = new BLLUsuario();
        bLLVenta = new BLLVenta();
        bLLProducto = new BLLProducto();
    }

    [WebMethod]
    public string GenerarXMLVentas(int[] idVentas)
    {
        var usuarios = bLLUsuario.GetUsuarios();
        var productos = bLLProducto.GetProductos();
        var ventas = bLLVenta.GetAll().Where(v => idVentas.Contains(v.Id)).ToList();

        // Usar StringWriter para capturar el XML en un string
        using (var stringWriter = new StringWriter())
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = System.Text.Encoding.UTF8,
                OmitXmlDeclaration = false // Para incluir la declaración XML
            };

            using (XmlWriter writer = XmlWriter.Create(stringWriter, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Ventas");

                // Iterar sobre las ventas seleccionadas y escribirlas en el XML
                foreach (var venta in ventas)
                {
                    writer.WriteStartElement("Venta");

                    writer.WriteElementString("Id", venta.Id.ToString());
                    writer.WriteElementString("FechaCreado", venta.FechaCreado.ToString("dd/MM/yyyy HH:mm"));
                    writer.WriteElementString("MetodoDePago", venta.MetodoDePago.ToString());
                    var usuario = venta.IdUsuario != null ? usuarios.FirstOrDefault(u => u.Id == venta.IdUsuario) : null;
                    writer.WriteElementString("Usuario", usuario != null ? usuario.Nombre : "N/A");
                    writer.WriteElementString("MontoTotal", venta.MontoTotal.ToString("F2"));

                    // Escribir los productos asociados a la venta
                    writer.WriteStartElement("Productos");
                    foreach (var producto in venta.VentaProductos)
                    {
                        var prod = productos.FirstOrDefault(p => p.Id == producto.Id);
                        writer.WriteStartElement("Producto");

                        writer.WriteElementString("Nombre", prod.Nombre);
                        writer.WriteElementString("Cantidad", producto.Cantidad.ToString());
                        writer.WriteElementString("Monto", producto.Monto.ToString("F2"));

                        writer.WriteEndElement(); // Cerrar Producto
                    }
                    writer.WriteEndElement(); // Cerrar Productos

                    writer.WriteEndElement(); // Cerrar Venta
                }

                writer.WriteEndElement(); // Cerrar Ventas
                writer.WriteEndDocument();
            }

            // Devolver el XML como string
            return stringWriter.ToString();
        }
    }
}

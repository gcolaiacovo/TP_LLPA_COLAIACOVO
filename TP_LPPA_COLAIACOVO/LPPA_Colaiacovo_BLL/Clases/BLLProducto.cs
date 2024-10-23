using LPPA_Colaiacovo_DAL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.Excepciones;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPPA_Colaiacovo_BLL.Clases
{
    public class BLLProducto
    {
        private readonly DALProducto dALProducto;

        public BLLProducto()
        {
            dALProducto = new DALProducto();
        }

        public List<Producto> GetProductos()
        {
            List<Producto> products = dALProducto.GetAll();

            foreach (var product in products)
            {
                var digitoVerificador = this.CalcularChecksum(product);
                if (digitoVerificador != product.DigitoVerificador)
                {
                    throw new DigitoVerificadorException(digitoVerificador, "Producto");
                }
            }

            return products;
        }

        public byte CalcularChecksum(Producto producto)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(producto.Nombre ?? string.Empty);
            sb.Append(producto.Descripcion ?? string.Empty);
            sb.Append(producto.Marca ?? string.Empty);
            sb.Append(producto.CategoriaId);
            sb.Append(producto.Precio);
            sb.Append(producto.Stock);
            sb.Append(producto.FechaCreado.ToString("yyyyMMdd"));

            if (producto.FechaModificado.HasValue)
            {
                sb.Append(producto.FechaModificado.Value.ToString("yyyyMMdd"));
            }

            string concatenatedString = sb.ToString();

            // Convertir la cadena a un array de bytes (usando valores ASCII)
            byte[] data = Encoding.ASCII.GetBytes(concatenatedString);

            // Calcular el checksum como el complemento a 8 bits de la suma de los bytes
            int sum = data.Sum(b => b);
            byte checksum = (byte)~sum;

            return checksum;
        }
    }
}

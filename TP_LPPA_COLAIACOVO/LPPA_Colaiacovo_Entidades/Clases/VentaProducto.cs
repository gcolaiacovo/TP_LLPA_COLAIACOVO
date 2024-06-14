using System;

namespace LPPA_Colaiacovo_Entidades.Clases
{
    [Serializable]
    public class VentaProducto
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public Venta Venta { get; set; }
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public DateTime? FechaModificado { get; set; }
    }
}

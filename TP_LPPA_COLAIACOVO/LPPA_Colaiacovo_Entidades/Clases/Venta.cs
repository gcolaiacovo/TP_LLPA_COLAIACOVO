using LPPA_Colaiacovo_Entidades.Enums;
using System;
using System.Collections.Generic;

namespace LPPA_Colaiacovo_Entidades.Clases
{
    [Serializable]
    public class Venta
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public double MontoTotal { get; set; }
        public MetodoDePagoEnum MetodoDePago { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public DateTime? FechaModificado { get; set; }

        public List<VentaProducto> VentaProductos { get; set; } = new List<VentaProducto>();
    }
}

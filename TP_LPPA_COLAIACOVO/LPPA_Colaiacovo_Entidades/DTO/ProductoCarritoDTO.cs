using LPPA_Colaiacovo_Entidades.Enums;
using System;

namespace LPPA_Colaiacovo_Entidades.DTO
{
    [Serializable]
    public class ProductoCarritoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public CategoriaProductoEnum CategoriaId { get; set; }
        public decimal Precio { get; set; }
        public string UrlImagen { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}

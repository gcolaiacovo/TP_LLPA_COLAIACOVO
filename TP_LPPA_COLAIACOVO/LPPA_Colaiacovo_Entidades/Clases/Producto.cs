using LPPA_Colaiacovo_Entidades.Enums;
using System;

namespace LPPA_Colaiacovo_Entidades.Clases
{
    [Serializable]
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public CategoriaProductoEnum CategoriaId { get; set; }
        public decimal Precio { get; set; }
        public string UrlImagen { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public DateTime? FechaModificado { get; set; }
        public int DigitoVerificador { get; set; }

    }
}

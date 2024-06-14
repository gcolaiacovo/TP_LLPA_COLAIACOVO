using System;

namespace LPPA_Colaiacovo_Entidades.Clases
{
    [Serializable]
    public class Bitacora
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int? IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public DateTime? FechaModificado { get; set; }
    }
}

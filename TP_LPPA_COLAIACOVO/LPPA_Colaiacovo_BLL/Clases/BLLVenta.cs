using LPPA_Colaiacovo_DAL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using System.Collections.Generic;

namespace LPPA_Colaiacovo_BLL.Clases
{
    public class BLLVenta
    {
        private readonly DALVenta dALVenta;

        public BLLVenta()
        {
            dALVenta = new DALVenta();
        }

        public Venta GetById(int id)
        {
            return dALVenta.Get(id);
        }

        public List<Venta> GetByUsuarioId(int id)
        {
            return dALVenta.GetByUsuarioId(id);
        }

        public List<Venta> GetAll()
        {
            return dALVenta.GetAll();
        }

        public void Save(Venta venta)
        {
            dALVenta.Save(venta);
        }
    }
}

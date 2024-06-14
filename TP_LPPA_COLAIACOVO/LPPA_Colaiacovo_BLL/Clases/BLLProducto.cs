using LPPA_Colaiacovo_DAL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using System.Collections.Generic;

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
            return dALProducto.GetAll();
        }
    }
}

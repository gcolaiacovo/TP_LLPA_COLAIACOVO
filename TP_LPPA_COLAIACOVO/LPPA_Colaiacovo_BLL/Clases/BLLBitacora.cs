using LPPA_Colaiacovo_DAL.Clases;
using LPPA_Colaiacovo_Entidades.Clases;
using System.Collections.Generic;

namespace LPPA_Colaiacovo_BLL.Clases
{
    public class BLLBitacora
    {
        private readonly DALBitacora DALBitacora;

        public BLLBitacora()
        {
            DALBitacora = new DALBitacora();
        }

        public List<Bitacora> GetBitacoras()
        {
            return DALBitacora.GetAll();
        }

        public void SaveBitacora(Bitacora bitacora)
        {
            DALBitacora.Save(bitacora);
        }
    }
}

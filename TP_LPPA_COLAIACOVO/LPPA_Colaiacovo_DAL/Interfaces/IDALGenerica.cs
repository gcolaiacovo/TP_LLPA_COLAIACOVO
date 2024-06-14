using System.Collections.Generic;

namespace LPPA_Colaiacovo_DAL.Interfaces
{
    public interface IDALGenerica<T> where T : class
    {
        void Save(T entity);
        List<T> GetAll();
        T Get(int  id);
        void Delete(int id);
    }
}

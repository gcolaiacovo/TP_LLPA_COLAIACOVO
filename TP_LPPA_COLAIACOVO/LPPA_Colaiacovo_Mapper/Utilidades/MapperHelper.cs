using System.Data.SqlClient;

namespace LPPA_Colaiacovo_Mapper.Utilidades
{
    public static class MapperHelper
    {
        public static T GetDataType<T>(this SqlDataReader r, string name, object def = null)
        {
            var col = r.GetOrdinal(name);
            return r.IsDBNull(col) ? (T)def : (T)r[name];
        }
    }
}

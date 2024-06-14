using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Mapper.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LPPA_Colaiacovo_Mapper.Clases
{
    public class MapperBitacora
    {
        public List<Bitacora> Map(SqlDataReader sqlDataReader)
        {
            var bitacoras = new List<Bitacora>();
            while (sqlDataReader.Read())
            {
                var id = MapperHelper.GetDataType<int>(sqlDataReader, "Id");
                var descripcion = MapperHelper.GetDataType<string>(sqlDataReader, "Descripcion");
                var idUsuario = MapperHelper.GetDataType<int?>(sqlDataReader, "IdUsuario");
                var activo = MapperHelper.GetDataType<bool>(sqlDataReader, "Activo");
                var fechaCreado = MapperHelper.GetDataType<DateTime>(sqlDataReader, "FechaCreado");
                var fechaModificado = MapperHelper.GetDataType<DateTime?>(sqlDataReader, "FechaModificado");

                var bitacora = new Bitacora
                {
                    Id = id,
                    Descripcion = descripcion,
                    IdUsuario = idUsuario,
                    Activo = activo,
                    FechaCreado = fechaCreado,
                    FechaModificado = fechaModificado
                };

                bitacoras.Add(bitacora);
            }

            return bitacoras;
        }
    }
}

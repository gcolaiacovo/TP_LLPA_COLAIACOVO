using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.Enums;
using LPPA_Colaiacovo_Mapper.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LPPA_Colaiacovo_Mapper.Clases
{
    public class MapperVenta
    {
        public List<Venta> Map(SqlDataReader sqlDataReader)
        {
            var ventas = new List<Venta>();
            var currentVenta = new Venta();

            while (sqlDataReader.Read())
            {
                var ventaId = MapperHelper.GetDataType<int>(sqlDataReader, "Id");

                if (currentVenta == null || currentVenta.Id != ventaId)
                {
                    currentVenta = new Venta
                    {
                        Id = ventaId,
                        IdUsuario = MapperHelper.GetDataType<int?>(sqlDataReader, "IdUsuario"),
                        MontoTotal = MapperHelper.GetDataType<decimal>(sqlDataReader, "MontoTotal"),
                        MetodoDePago = MapperHelper.GetDataType<MetodoDePagoEnum>(sqlDataReader, "MetodoDePago"),
                        Activo = MapperHelper.GetDataType<bool>(sqlDataReader, "Activo"),
                        FechaCreado = MapperHelper.GetDataType<DateTime>(sqlDataReader, "FechaCreado"),
                        FechaModificado = MapperHelper.GetDataType<DateTime?>(sqlDataReader, "FechaModificado"),
                        VentaProductos = new List<VentaProducto>()
                    };
                    ventas.Add(currentVenta);
                }

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("IdProducto")))
                {
                    var ventaProducto = new VentaProducto
                    {
                        Id = MapperHelper.GetDataType<int>(sqlDataReader, "Id"),
                        IdVenta = MapperHelper.GetDataType<int>(sqlDataReader, "IdVenta"),
                        IdProducto = MapperHelper.GetDataType<int>(sqlDataReader, "IdProducto"),
                        Cantidad = MapperHelper.GetDataType<int>(sqlDataReader, "Cantidad"),
                        Monto = MapperHelper.GetDataType<decimal>(sqlDataReader, "Monto"),
                        Activo = MapperHelper.GetDataType<bool>(sqlDataReader, "Activo"),
                        FechaCreado = MapperHelper.GetDataType<DateTime>(sqlDataReader, "FechaCreado"),
                        FechaModificado = MapperHelper.GetDataType<DateTime?>(sqlDataReader, "FechaModificado")
                    };
                    currentVenta.VentaProductos.Add(ventaProducto);
                }
            }

            return ventas;
        }
    }
}

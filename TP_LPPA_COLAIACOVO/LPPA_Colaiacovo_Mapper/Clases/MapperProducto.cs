using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Entidades.Enums;
using LPPA_Colaiacovo_Mapper.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LPPA_Colaiacovo_Mapper.Clases
{
    public class MapperProducto
    {
        public List<Producto> Map(SqlDataReader sqlDataReader)
        {
            var productos = new List<Producto>();
            while (sqlDataReader.Read())
            {
                var id = MapperHelper.GetDataType<int>(sqlDataReader, "Id");
                var nombre = MapperHelper.GetDataType<string>(sqlDataReader, "Nombre");
                var descripcion = MapperHelper.GetDataType<string>(sqlDataReader, "Descripcion");
                var marca = MapperHelper.GetDataType<string>(sqlDataReader, "Marca");
                var categoriaId = MapperHelper.GetDataType<int>(sqlDataReader, "CategoriaId");
                var precio = MapperHelper.GetDataType<decimal>(sqlDataReader, "Precio");
                var urlImagen = MapperHelper.GetDataType<string>(sqlDataReader, "UrlImagen");
                var stock = MapperHelper.GetDataType<int>(sqlDataReader, "Stock");
                var activo = MapperHelper.GetDataType<bool>(sqlDataReader, "Activo");
                var fechaCreado = MapperHelper.GetDataType<DateTime>(sqlDataReader, "FechaCreado");
                var fechaModificado = MapperHelper.GetDataType<DateTime?>(sqlDataReader, "FechaModificado");
                var digitoVerificador = MapperHelper.GetDataType<int>(sqlDataReader, "DigitoVerificador");

                var producto = new Producto
                {
                    Id = id,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Marca = marca,
                    CategoriaId = (CategoriaProductoEnum)categoriaId,
                    Precio = precio,
                    UrlImagen = urlImagen,
                    Stock = stock,
                    Activo = activo,
                    FechaCreado = fechaCreado,
                    FechaModificado = fechaModificado,
                    DigitoVerificador = digitoVerificador
                };

                productos.Add(producto);
            }

            return productos;
        }
    }
}

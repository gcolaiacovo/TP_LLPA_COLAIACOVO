using LPPA_Colaiacovo_DAL.Interfaces;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Mapper.Clases;
using LPPA_Colaiacovo_Services.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace LPPA_Colaiacovo_DAL.Clases
{
    public class DALVenta : IDALGenerica<Venta>
    {
        private readonly MapperVenta mapperVenta;
        public DALVenta()
        {
            this.mapperVenta = new MapperVenta();
        }

        public void Delete(int id)
        {
            var command = "DELETE FROM Venta WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public Venta Get(int id)
        {
            var command = @"
                SELECT v.*, vp.*
                FROM Venta v
                LEFT JOIN VentaProducto vp ON v.Id = vp.IdVenta
                WHERE v.Id = @Id";

            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    var reader = sqlCommand.ExecuteReader();
                    return mapperVenta.Map(reader).FirstOrDefault();
                }
            }
        }

        public List<Venta> GetAll()
        {
            var command = @"
                SELECT v.*, vp.*
                FROM Venta v
                LEFT JOIN VentaProducto vp ON v.Id = vp.IdVenta";

            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    return mapperVenta.Map(reader);
                }
            }
        }

        public List<Venta> GetByUsuarioId(int usuarioId)
        {
            var command = @"
                SELECT v.*, vp.*
                FROM Venta v
                LEFT JOIN VentaProducto vp ON v.Id = vp.IdVenta
                WHERE v.IdUsuario = @UsuarioId";

            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    var reader = sqlCommand.ExecuteReader();
                    return mapperVenta.Map(reader);
                }
            }
        }

        public void Save(Venta entity)
        {
            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sqlBuilder = new StringBuilder();
                        sqlBuilder.AppendLine("DECLARE @IdVenta INT;"); 

                        sqlBuilder.AppendLine("INSERT INTO Venta (IdUsuario, MontoTotal, MetodoDePago, Activo, FechaCreado) ");
                        sqlBuilder.AppendLine("VALUES (@IdUsuario, @MontoTotal, @MetodoDePago, @Activo, @FechaCreado);");
                        sqlBuilder.AppendLine("SET @IdVenta = SCOPE_IDENTITY();");

                        sqlBuilder.AppendLine("DECLARE @VentaProductos TABLE (IdProducto INT, Cantidad INT, Monto DECIMAL(18, 2), Activo BIT, FechaCreado DATETIME);");

                        foreach (var ventaProducto in entity.VentaProductos)
                        {
                            sqlBuilder.AppendLine("INSERT INTO VentaProducto (IdVenta, IdProducto, Cantidad, Monto, Activo, FechaCreado) VALUES ");
                            sqlBuilder.AppendLine($"(@IdVenta, {ventaProducto.IdProducto}, {ventaProducto.Cantidad}, {ventaProducto.Monto.ToString(CultureInfo.InvariantCulture)}, {(ventaProducto.Activo ? 1 : 0)}, '{ventaProducto.FechaCreado.ToString("yyyy-MM-dd HH:mm:ss")}');");
                        }

                        using (SqlCommand sqlCommand = new SqlCommand(sqlBuilder.ToString(), connection, transaction))
                        {
                            sqlCommand.Parameters.AddWithValue("@IdUsuario", (object)entity.IdUsuario ?? DBNull.Value);
                            sqlCommand.Parameters.AddWithValue("@MontoTotal", entity.MontoTotal);
                            sqlCommand.Parameters.AddWithValue("@MetodoDePago", (int)entity.MetodoDePago);
                            sqlCommand.Parameters.AddWithValue("@Activo", entity.Activo);
                            sqlCommand.Parameters.AddWithValue("@FechaCreado", entity.FechaCreado);

                            sqlCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
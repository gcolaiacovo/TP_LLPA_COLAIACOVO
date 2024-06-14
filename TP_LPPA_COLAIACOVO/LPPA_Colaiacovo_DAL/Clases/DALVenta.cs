using LPPA_Colaiacovo_DAL.Interfaces;
using LPPA_Colaiacovo_DAL.Utilidades;
using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Mapper.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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
            var command = "DELETE FROM Ventas WHERE Id = @Id";
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
                FROM Ventas v
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
                FROM Ventas v
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
                FROM Ventas v
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
            var command = entity.Id == 0 ?
                "INSERT INTO Ventas (IdUsuario, MontoTotal, MetodoDePago, Activo, FechaCreado, FechaModificado) VALUES (@IdUsuario, @MontoTotal, @MetodoDePago, @Activo, @FechaCreado, @FechaModificado)" :
                "UPDATE Ventas SET IdUsuario = @IdUsuario, MontoTotal = @MontoTotal, MetodoDePago = @MetodoDePago, Activo = @Activo, FechaModificado = @FechaModificado WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(command, connection, transaction))
                        {
                            sqlCommand.Parameters.AddWithValue("@IdUsuario", (object)entity.IdUsuario ?? DBNull.Value);
                            sqlCommand.Parameters.AddWithValue("@MontoTotal", entity.MontoTotal);
                            sqlCommand.Parameters.AddWithValue("@MetodoDePago", entity.MetodoDePago);
                            sqlCommand.Parameters.AddWithValue("@Activo", entity.Activo);
                            sqlCommand.Parameters.AddWithValue("@FechaCreado", entity.FechaCreado);
                            sqlCommand.Parameters.AddWithValue("@FechaModificado", (object)entity.FechaModificado ?? DBNull.Value);

                            if (entity.Id != 0)
                            {
                                sqlCommand.Parameters.AddWithValue("@Id", entity.Id);
                            }

                            sqlCommand.ExecuteNonQuery();
                        }

                        foreach (var ventaProducto in entity.VentaProductos)
                        {
                            var commandVentaProducto = ventaProducto.Id == 0 ?
                                "INSERT INTO VentaProducto (IdVenta, IdProducto, Cantidad, Monto, Activo, FechaCreado, FechaModificado) VALUES (@IdVenta, @IdProducto, @Cantidad, @Monto, @Activo, @FechaCreado, @FechaModificado)" :
                                "UPDATE VentaProducto SET IdVenta = @IdVenta, IdProducto = @IdProducto, Cantidad = @Cantidad, Monto = @Monto, Activo = @Activo, FechaModificado = @FechaModificado WHERE Id = @Id";

                            using (SqlCommand sqlCommandVentaProducto = new SqlCommand(commandVentaProducto, connection, transaction))
                            {
                                sqlCommandVentaProducto.Parameters.AddWithValue("@IdVenta", entity.Id);
                                sqlCommandVentaProducto.Parameters.AddWithValue("@IdProducto", ventaProducto.IdProducto);
                                sqlCommandVentaProducto.Parameters.AddWithValue("@Cantidad", ventaProducto.Cantidad);
                                sqlCommandVentaProducto.Parameters.AddWithValue("@Monto", ventaProducto.Monto);
                                sqlCommandVentaProducto.Parameters.AddWithValue("@Activo", ventaProducto.Activo);
                                sqlCommandVentaProducto.Parameters.AddWithValue("@FechaCreado", ventaProducto.FechaCreado);
                                sqlCommandVentaProducto.Parameters.AddWithValue("@FechaModificado", (object)ventaProducto.FechaModificado ?? DBNull.Value);

                                if (ventaProducto.Id != 0)
                                {
                                    sqlCommandVentaProducto.Parameters.AddWithValue("@Id", ventaProducto.Id);
                                }

                                sqlCommandVentaProducto.ExecuteNonQuery();
                            }
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
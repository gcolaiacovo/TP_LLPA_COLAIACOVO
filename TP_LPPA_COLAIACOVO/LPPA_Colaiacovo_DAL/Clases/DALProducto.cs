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
    public class DALProducto : IDALGenerica<Producto>
    {
        private readonly MapperProducto mapperProducto;

        public DALProducto()
        {
            this.mapperProducto = new MapperProducto();
        }

        public void Delete(int id)
        {
            var command = "DELETE FROM Producto WHERE Id = @Id";
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

        public Producto Get(int id)
        {
            var command = "SELECT * FROM Producto WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);
                    var reader = sqlCommand.ExecuteReader();
                    return mapperProducto.Map(reader).FirstOrDefault();
                }
            }
        }

        public List<Producto> GetAll()
        {
            var command = "SELECT * FROM Producto";
            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    return mapperProducto.Map(reader);
                }
            }
        }

        public void Save(Producto entity)
        {
            var command = entity.Id == 0
                ? "INSERT INTO Producto (Nombre, Descripcion, Marca, CategoriaId, Precio, UrlImagen, Stock, Activo, FechaCreado, FechaModificado) VALUES (@Nombre, @Descripcion, @Marca, @CategoriaId, @Precio, @UrlImagen, @Stock, @Activo, @FechaCreado, @FechaModificado)"
                : "UPDATE Producto SET Nombre = @Nombre, Descripcion = @Descripcion, Marca = @Marca, CategoriaId = @CategoriaId, Precio = @Precio, UrlImagen = @UrlImagen, Stock = @Stock, Activo = @Activo, FechaModificado = @FechaModificado WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(SQLHelper.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Nombre", entity.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", entity.Descripcion);
                    sqlCommand.Parameters.AddWithValue("@Marca", entity.Marca);
                    sqlCommand.Parameters.AddWithValue("@CategoriaId", entity.CategoriaId);
                    sqlCommand.Parameters.AddWithValue("@Precio", entity.Precio);
                    sqlCommand.Parameters.AddWithValue("@UrlImagen", (object)entity.UrlImagen ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@Stock", entity.Stock);
                    sqlCommand.Parameters.AddWithValue("@Activo", entity.Activo);
                    sqlCommand.Parameters.AddWithValue("@FechaCreado", entity.FechaCreado);
                    sqlCommand.Parameters.AddWithValue("@FechaModificado", (object)entity.FechaModificado ?? DBNull.Value);

                    if (entity.Id != 0)
                    {
                        sqlCommand.Parameters.AddWithValue("@Id", entity.Id);
                    }

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
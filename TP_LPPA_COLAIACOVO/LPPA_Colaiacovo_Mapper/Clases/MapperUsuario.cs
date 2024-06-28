using LPPA_Colaiacovo_Entidades.Clases;
using LPPA_Colaiacovo_Mapper.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LPPA_Colaiacovo_Mapper.Clases
{
    public class MapperUsuario
    {
        public List<Usuario> Map(SqlDataReader sqlDataReader)
        {
            var usuarios = new List<Usuario>();
            while (sqlDataReader.Read())
            {
                var id = MapperHelper.GetDataType<int>(sqlDataReader, "Id");
                var nombre = MapperHelper.GetDataType<string>(sqlDataReader, "Nombre");
                var apellido = MapperHelper.GetDataType<string>(sqlDataReader, "Apellido");
                var email = MapperHelper.GetDataType<string>(sqlDataReader, "Email");
                var contrasena = MapperHelper.GetDataType<string>(sqlDataReader, "Contrasena");
                var fechaNacimiento = MapperHelper.GetDataType<DateTime?>(sqlDataReader, "FechaNacimiento");
                var rol = MapperHelper.GetDataType<string>(sqlDataReader, "Rol");
                var activo = MapperHelper.GetDataType<bool>(sqlDataReader, "Activo");
                var fechaCreado = MapperHelper.GetDataType<DateTime>(sqlDataReader, "FechaCreado");
                var fechaModificado = MapperHelper.GetDataType<DateTime?>(sqlDataReader, "FechaModificado");
                var digitoVerificador = MapperHelper.GetDataType<int>(sqlDataReader, "DigitoVerificador");
                var usuario = new Usuario(id, nombre, apellido, email, contrasena, fechaNacimiento, rol, activo, fechaCreado, fechaModificado, digitoVerificador);
                usuarios.Add(usuario);
            }

            return usuarios;
        }
    }
}

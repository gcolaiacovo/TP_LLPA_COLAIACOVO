using System;

namespace LPPA_Colaiacovo_Entidades.Excepciones
{
    public class DigitoVerificadorException : Exception
    {
        public int Id { get; }
        public string EntidadTipo { get; }


        public DigitoVerificadorException(int id, string entidadTipo = "Usuario")
        {
            Id = id;
            EntidadTipo = entidadTipo;
        }

        public override string Message
        {
            get
            {
                return $"El {EntidadTipo} {Id} está corrompido!!";
            }
        }
    }
}

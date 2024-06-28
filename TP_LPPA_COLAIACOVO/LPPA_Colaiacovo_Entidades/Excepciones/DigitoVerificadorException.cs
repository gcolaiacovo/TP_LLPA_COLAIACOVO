using System;

namespace LPPA_Colaiacovo_Entidades.Excepciones
{
    public class DigitoVerificadorException : Exception
    {
        public int Id { get; }

        public DigitoVerificadorException(int id)
        {
            Id = id;
        }

        public override string Message
        {
            get
            {
                return $"El usuario {Id} está corrompido!!";
            }
        }
    }
}

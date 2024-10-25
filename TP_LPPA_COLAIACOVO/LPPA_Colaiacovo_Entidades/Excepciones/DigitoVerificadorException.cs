using System;
using System.Collections.Generic;

namespace LPPA_Colaiacovo_Entidades.Excepciones
{
    public class DigitoVerificadorException : Exception
    {
        public List<int> Ids { get; }
        public string EntidadTipo { get; }

        public DigitoVerificadorException(List<int> ids, string entidadTipo = "Usuario")
        {
            Ids = ids;
            EntidadTipo = entidadTipo;
        }

        public override string Message
        {
            get
            {
                return $"El {EntidadTipo} {Ids} está corrompido!!";
            }
        }
    }
}

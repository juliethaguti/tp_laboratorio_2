using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class DniInvalidoException : Exception
    {
        private static string mensajeBase;

        public DniInvalidoException()
            : base("dni inválido")
        {

        }

        public DniInvalidoException(Exception e)
            : this("Dni inválido", e)
        { }

        public DniInvalidoException(string message)
            : this(message, null)
        {
            mensajeBase = message;
        }

        public DniInvalidoException(string message, Exception e)
            : base(message, e)
        { }
    }
}

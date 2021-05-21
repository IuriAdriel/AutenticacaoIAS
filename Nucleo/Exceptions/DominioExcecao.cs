using System;
using System.Collections.Generic;

namespace Nucleo.Exceptions
{
    public class DominioExcecao : Exception
    {
        internal List<string> _errors;

        public IReadOnlyCollection<string> Errors => _errors;

        public DominioExcecao()
        { }

        public DominioExcecao(string message, List<string> errors) : base(message)
        {
            _errors = errors;
        }

        public DominioExcecao(string message) : base(message)
        { }

        public DominioExcecao(string message, Exception innerException) : base(message, innerException)
        { }
    }
}

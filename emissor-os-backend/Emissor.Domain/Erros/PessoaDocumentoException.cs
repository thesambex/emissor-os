using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Domain.Erros;

public class PessoaDocumentoException : Exception
{

    public PessoaDocumentoException() { }

    public PessoaDocumentoException(string message) : base(message) { }

    public PessoaDocumentoException(string message, Exception inner) : base(message, inner) { }

}

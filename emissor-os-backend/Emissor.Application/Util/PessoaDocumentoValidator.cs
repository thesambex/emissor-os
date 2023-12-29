using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Util;

public abstract class PessoaDocumentoValidator
{

    protected string document;

    public PessoaDocumentoValidator(string document)
    {
        this.document = document;
    }

    public abstract bool IsValido();

    // Essa função só deverá ser chamada após IsValido() ser chamado e ter retornado true
    public abstract string GetDocumentoValido();

}

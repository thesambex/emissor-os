using Emissor.Application.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Util;

public class CNPJDocumentoValidator : PessoaDocumentoValidator
{

    public CNPJDocumentoValidator(string cnpj)
        : base(cnpj)
    {

    }

    public override bool IsValido()
    {
        throw new NotImplementedException();
    }

    public override string GetDocumentoValido() => document;

}

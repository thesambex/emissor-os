using Emissor.Application.Util;
using Emissor.Infra.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Factory;

public class PessoaDocumentoValidatorFactory
{

    public static PessoaDocumentoValidator Create(string documento, bool isPj) => isPj ? new CNPJDocumentoValidator(documento) : new CPFDocumentoValidator(documento);

}

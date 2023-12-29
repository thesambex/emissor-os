using Emissor.Infra.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Test.Util;

public class PessoaDocumentoTest
{

    [Fact]
    public void Deve_Retornar_Um_CPF_Invalido_Digitos_Iguais()
    {
        var validator = PessoaDocumentoValidatorFactory.Create("111.111.111-11", false);
        Assert.False(validator.IsValido());
    }

    [Fact]
    public void Deve_Retornar_Um_CPF_Valido()
    {
        var validator = PessoaDocumentoValidatorFactory.Create("Coloque um CPF Válido", false);
        Assert.True(validator.IsValido());
        Assert.Equal(11, validator.GetDocumentoValido().Length);
    }

    [Fact]
    public void Deve_Retornar_Um_CPF_Invalido()
    {
        var validator = PessoaDocumentoValidatorFactory.Create("100.000.000-00", false);
        Assert.False(validator.IsValido());
    }

}

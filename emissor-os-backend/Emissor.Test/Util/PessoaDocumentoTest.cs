using Emissor.Application.Util;
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
        var validator = PessoaDocumentoValidatorFactory.Create("Insira um CPF válido", false);
        Assert.True(validator.IsValido());
        Assert.Equal(11, validator.GetDocumentoValido().Length);
    }

    [Fact]
    public void Deve_Retornar_Um_CPF_Invalido()
    {
        var validator = PessoaDocumentoValidatorFactory.Create("100.000.000-00", false);
        Assert.False(validator.IsValido());
    }

    [Fact]
    public void Deve_Retornar_Um_CNPJ_Invalido_Digitos_Iguais()
    {
        var validator = PessoaDocumentoValidatorFactory.Create("11.111.111/1111-11", true);
        Assert.False(validator.IsValido());
    }

    [Fact]
    public void Deve_Retornar_Um_CNPJ_Valido()
    {
        var validator = PessoaDocumentoValidatorFactory.Create("Insira um CNPJ válido", true);
        Assert.True(validator.IsValido());
        Assert.Equal(14, validator.GetDocumentoValido().Length);
    }

    [Fact]
    public void Deve_Retornar_Um_CNPJ_Invalido()
    {
        var validator = PessoaDocumentoValidatorFactory.Create("1.234.567/8910-11", true);
        Assert.False(validator.IsValido());
    }

}

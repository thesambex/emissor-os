using Emissor.Application.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Emissor.Infra.Util;

public class CPFDocumentoValidator : PessoaDocumentoValidator
{

    public CPFDocumentoValidator(string cpf) 
        : base(cpf)
    {

    }

    public override bool IsValido()
    {
        if (Regex.IsMatch(documento, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
        {
            documento = Regex.Replace(documento, @"[^\d]", "");
        }

        if(documento.Length != 11) return false;
        if (documento.All(d => d == documento[0])) return false;

        return IsDVValido();
    }

    public override string GetDocumentoValido() => documento;

    private int CalcularDV1()
    {
        int soma = 0;
        for(int i = 0; i < 9; i++)
        {
            soma += int.Parse(documento[i].ToString()) * (10 - i);
        }

        int resto = soma % 11;
        return (resto < 2) ? 0 : 11 - resto;
    }

    private int CalcularDV2() 
    {
        int soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(documento[i].ToString()) * (11 - i);
        }

        int resto = soma % 11;
        return (resto < 2) ? 0 : 11 - resto;
    }

    private bool IsDVValido()
    {
        int dv1 = int.Parse(documento[9].ToString());
        int dv2 = int.Parse(documento[10].ToString());

        return dv1 == CalcularDV1() && dv2 == CalcularDV2();
    }

}

using Emissor.Application.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        if(Regex.IsMatch(documento, @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"))
        {
            documento = Regex.Replace(documento, @"[^\d]", "");
        }

        if (documento.Length != 14) return false;
        if (documento.All(d => d == documento[0])) return false;

        return true;
    }

    public override string GetDocumentoValido() => documento;

    private int CalcularDV1()
    {
        int soma = 0;
        for(int i = 0; i < 12; i++)
        {
            soma += int.Parse(documento[i].ToString()) * (5 - (i % 6));
        }

        int resto = soma % 11;
        return (resto < 2) ? 0 : 11 - resto;
    }

    private int CalcularDV2() 
    {
        int soma = 0;
        for (int i = 0; i < 13; i++)
        {
            soma += int.Parse(documento[i].ToString()) * (6 - (i % 6));
        }

        int resto = soma % 11;
        return (resto < 2) ? 0 : 11 - resto;
    }

    private bool IsDVValido()
    {
        int dv1 = int.Parse(documento[12].ToString());
        int dv2 = int.Parse(documento[13].ToString());

        return dv1 == CalcularDV1() && dv2 == CalcularDV2();
    }

}

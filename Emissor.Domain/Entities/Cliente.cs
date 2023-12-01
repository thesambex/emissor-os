using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Domain.Entities;

public class Cliente
{

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Documento { get; set; }
    public string Endereco { get; set; }
    public int EnderecoNumero { get; set; }
    public string Bairro { get; set; }
    public string Municipio { get; set; }
    public bool IsPJ { get; set; }

}

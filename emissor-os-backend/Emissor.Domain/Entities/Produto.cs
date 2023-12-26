using Emissor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Domain.Entities;

public class Produto
{

    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public string Referencia { get; set; }
    public string CodigoBarra { get; set; }
    public TipoUnidades Unidade { get; set; }

}

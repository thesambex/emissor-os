using Emissor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Domain.Entities;

public class Mercadoria
{

    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public string Referencia { get; set; }
    public string? CodigoBarra { get; set; }
    public double Preco { get; set; }
    public TipoUnidades Unidade { get; set; }

    public OrdemServicoMercadoria? OrdemServicoMercadoria { get; set; }
}

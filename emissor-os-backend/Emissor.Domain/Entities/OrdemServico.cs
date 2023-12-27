using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Domain.Entities;

public class OrdemServico
{

    public Guid Id { get; set; }
    public long Numero { get; set; }
    public Guid ClienteId { get; set; }
    public Guid AtendenteId { get; set; }
    public string Descricao { get; set; }
    public string? Observacoes { get; set; }
    public double ValorHora { get; set; }
    public double ValorFinal { get; set; }
    public DateTime DtInicio { get; set; }
    public DateTime? DtFim { get; set; }

    public ICollection<OrdemServicoMercadoria>? OrdemServicoMercadorias { get; set; }

}

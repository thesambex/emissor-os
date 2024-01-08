using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Domain.Entities;

public class OrdemServicoMercadoria
{

    public Guid Id { get; set; }
    public Guid MercadoriaId { get; set; }
    public Guid OrdemServicoId { get; set; }
    public double Quantidade { get; set; }

    public OrdemServico? OrdemServico { get; set; }
    public Mercadoria? Mercadoria { get; set; }

}

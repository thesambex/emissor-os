﻿using System;
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
    public DateTimeOffset DtInicio { get; set; }
    public DateTimeOffset? DtFim { get; set; }

    public Cliente? Cliente { get; set; }
    public Usuario? Usuario { get; set; }
    public ICollection<OrdemServicoMercadoria>? OrdemServicoMercadorias { get; set; }

    public double ValorTotalHoras()
    {
        if(DtFim == null) { return 0; }
        var timeDiff = DtFim - DtInicio;
        return timeDiff.Value.TotalHours * ValorHora;
    }

}

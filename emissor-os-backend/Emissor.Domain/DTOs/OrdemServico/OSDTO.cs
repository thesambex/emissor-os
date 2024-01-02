using Emissor.Domain.DTOs.Clientes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.OrdemServico;

public record OSDTO(
    Guid Id, long Numero,
    [property:JsonPropertyName("atendente_id")]
    Guid AtendenteId,
    string Descricao,
    string? Observacoes,
    [property:JsonPropertyName("valor_hora")]
    double ValorHora,
    [property:JsonPropertyName("valor_final")]
    double? ValorFinal,
    [property:JsonPropertyName("dt_inicio")]
    DateTimeOffset DtInicio,
    [property:JsonPropertyName("dt_final")]
    DateTimeOffset? DtFinal,
    ClienteDTO Cliente
);

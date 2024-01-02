using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.OrdemServico;

public record AbrirOSDTO(
    Guid? Id,
    long? Numero,
    [Required(ErrorMessage = "Forneça o ID do cliente")]
    [property:JsonPropertyName("cliente_id")]
    Guid? ClienteId,
    [property:JsonPropertyName("atendente_id")]
    Guid? AtendenteId,
    [Required(ErrorMessage = "Descreva que tipo de serviço será realizado")]
    string Descricao,
    string? Observacoes,
    [Required(ErrorMessage = "Informe o valor cobrado por hora")]
    [property:JsonPropertyName("valor_hora")]
    double? ValorHora,
    [Required(ErrorMessage = "Informe a data e a hora de início")]
    [property:JsonPropertyName("dt_inicio")]
    DateTimeOffset? DtInicio
);

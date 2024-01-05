using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.OrdemServico;

public record MercadoriaOSDTO(
    Guid? Id,
    [property:JsonPropertyName("ordem_servico_id")]
    Guid? OrdemServicoId,
    [property:JsonPropertyName("mercadoria_id")]
    Guid MercadoriaId,
    string? Descricao,
    double Quantidade
);

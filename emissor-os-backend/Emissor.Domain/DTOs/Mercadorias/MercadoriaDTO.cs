using Emissor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.Mercadorias;

public record MercadoriaDTO(
    Guid? Id,
    [Required(ErrorMessage = "Informe a descrição da mercadoria")]
    [StringLength(maximumLength:60, ErrorMessage = "O Tamanho máximo é de 60 caracteres")]
    string Descricao,
    [Required(ErrorMessage = "Informe a referência da mercadoria")]
    [StringLength(maximumLength:10, ErrorMessage = "O Tamanho máximo é de 10 caracteres")]
    string Referencia,
    [StringLength(maximumLength:13, ErrorMessage = "O Tamanho máximo é de 13 caracteres")]
    [property:JsonPropertyName("codigo_barra")]
    string? CodigoBarra,
    [Required(ErrorMessage = "Informe o preço da mercadoria")]
    double? Preco,
    [Required(ErrorMessage = "Informe o tipo de unidade da mercadoria")]
    string Unidade    
);

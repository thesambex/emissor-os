using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.Clientes;

public record ClienteDTO(
    Guid? Id,
    [Required(ErrorMessage = "Forneça um nome para o cliente")]
    [StringLength(maximumLength:60, ErrorMessage = "O nome do cliente deve ter no máximo 60 caracteres")]
    string Nome,
    [Required(ErrorMessage = "Forneça o endereço")]
    [StringLength(maximumLength:18, ErrorMessage = "O documento do cliente deve conter no máximo 18 caracteres")]
    string Documento,
    [Required(ErrorMessage = "Forneça o endereço")]
    [StringLength(maximumLength:60, ErrorMessage = "O endereco deve conter até 60 caracteres")]
    string Endereco,
    [Required(ErrorMessage = "Forneça o número do endereço")]
    [property:JsonPropertyName("endereco_numero")]
    int EnderecoNumero,
    [Required(ErrorMessage = "Forneça o nome do bairro")]
    [StringLength(maximumLength:60, ErrorMessage = "O bairro deve conter até 60 caracteres")]
    string Bairro,
    [Required(ErrorMessage = "Forneça o município")]
    [StringLength(maximumLength:60, ErrorMessage = "O município deve conter até 60 caracteres")]
    string Municipio,
    [Required(ErrorMessage = "Informe se é PJ ou não")]
    bool IsPJ
);

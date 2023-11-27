using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs;

public sealed class CriarUsuarioDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Forneça um nome")]
    [StringLength(maximumLength:60, ErrorMessage = "Forneça um nome de até 60 caracteres")]
    [JsonPropertyName("nome")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Forneça um nome de usuário")]
    [StringLength(maximumLength:20, ErrorMessage = "Forneça um nome de usuário de até 20 caracteres")]
    [JsonPropertyName("nome_usuario")]
    public string NomeUsuario { get; set; }
    [Required(ErrorMessage = "Forneça uma senha")]
    [StringLength(maximumLength:12, MinimumLength = 6, ErrorMessage = "A senha deve possuir de 6 a 12 caracteres")]
    [JsonPropertyName("senha")]
    public string? Senha { get; set; }

}

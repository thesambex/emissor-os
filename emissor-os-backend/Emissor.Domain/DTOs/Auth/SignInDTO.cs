using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.Auth;

public record SignInDTO(

    [Required(ErrorMessage = "Forneça um nome de usuário")]
    [StringLength(maximumLength: 20, ErrorMessage = "Forneça um nome de usuário de até 20 caracteres")]
    [property:JsonPropertyName("nome_usuario")]
    string NomeUsuario,
    [Required(ErrorMessage = "Forneça uma senha")]
    [StringLength(maximumLength: 12, MinimumLength = 6, ErrorMessage = "A senha deve possuir de 6 a 12 caracteres")]
    [property:JsonPropertyName("senha")]
    string Senha
);

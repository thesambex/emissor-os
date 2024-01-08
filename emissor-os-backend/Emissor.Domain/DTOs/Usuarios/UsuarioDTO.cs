using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.Usuarios;

public record UsuarioDTO(Guid Id, string Nome, [property: JsonPropertyName("nome_usuario")] string NomeUsuario);

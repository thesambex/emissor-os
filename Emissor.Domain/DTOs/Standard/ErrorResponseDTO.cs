using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.Standard;

public class ErrorResponseDTO
{

    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("field")]
    public string? Field { get; set; }
    [JsonPropertyName("extra")]
    public object? Extra { get; set; }

}

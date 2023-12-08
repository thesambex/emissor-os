using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.Auth;

public class TokenDTO
{

    [JsonPropertyName("token")]
    public string Token { get; set; }

}

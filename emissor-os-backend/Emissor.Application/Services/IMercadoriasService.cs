using Emissor.Domain.DTOs.Mercadorias;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Services;

public interface IMercadoriasService
{
    Task<IActionResult> CriarMercadoria(MercadoriaDTO body);
    Task<IActionResult> GetMercadoria(Guid id);
}

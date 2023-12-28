using Emissor.Domain.DTOs.Clientes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Services;

public interface IClientesService
{
    Task<IActionResult> CriarCliente(ClienteDTO body);
    Task<IActionResult> GetClienteById(Guid id);
    Task<IActionResult> DeletarCliente(Guid id);
    Task<IActionResult> BuscarCliente(string query);
}

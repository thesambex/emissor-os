using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.Clientes;

public record ClienteBuscaDTO(Guid id, string nome, string documento);

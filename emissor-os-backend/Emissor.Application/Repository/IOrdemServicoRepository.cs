using Emissor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Repository;

public interface IOrdemServicoRepository
{
    Task<OrdemServico> CriarOS(OrdemServico ordemServico);
}

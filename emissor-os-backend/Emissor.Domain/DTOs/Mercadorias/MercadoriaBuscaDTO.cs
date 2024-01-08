using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Domain.DTOs.Mercadorias;

public record MercadoriaBuscaDTO(Guid Id, string Descricao, string Referencia, double Preco);

using Emissor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Repository;

public interface IMercadoriaRepository
{
    Task<Mercadoria> CriarMercadoria(Mercadoria mercadoria);
    Task<Mercadoria?> GetMercadoria(Guid id);
    Task<bool> IssetMercadoriaByReferencia(string referencia);
    Task<bool> IssetMercadoriaByCodigoBarra(string codigoBarra);
    Task<bool> DeletarMercadoria(Guid id);
    Task<List<Mercadoria>> BuscarMercadoria(string query);
    Task<Mercadoria?> GetMercadoriaCodigoBarra(string codigoBarra);
}

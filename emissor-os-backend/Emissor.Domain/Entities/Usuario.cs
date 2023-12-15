using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Domain.Entities;

public class Usuario
{

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string NomeUsuario { get; set; }
    public string Senha { get; set; }

}

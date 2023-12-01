using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Repository;

public interface IUnitOfWork
{
    Task Begin();
    Task Commit();
    Task Rollback();
}

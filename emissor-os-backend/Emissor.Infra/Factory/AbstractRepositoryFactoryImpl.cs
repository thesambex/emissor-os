﻿using Emissor.Application.Database;
using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Factory;

public class AbstractRepositoryFactoryImpl : IAbstractRepositoryFactory
{

    private readonly PgContext _pgContext;

    public AbstractRepositoryFactoryImpl(PgContext pgContext)
    {
        _pgContext = pgContext;
    }

    public IUsuariosRepository CreateUsuariosRepository()
    {
        return new UsuariosRepositorryImpl(_pgContext);
    }

}
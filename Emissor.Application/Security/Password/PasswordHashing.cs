using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Security.Password;

public class PasswordHashing
{

    private readonly IPasswordHashStrategy _strategy;

    public PasswordHashing(IPasswordHashStrategy strategy)
    {
        _strategy = strategy;
    }

    public string Hash(string password)
    {
        return _strategy.Hash(password);
    }

    public bool Verify(string password, string hash) 
    {
        return _strategy.Verify(password, hash);
    }

}

using Emissor.Application.Security.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Emissor.Infra.Security.Password;

internal class BCryptPasswordHashStrategy : IPasswordHashStrategy
{
    public string Hash(string password) => BC.HashPassword(password, 12); 

    public bool Verify(string password, string hash) => BC.Verify(password, hash);

}

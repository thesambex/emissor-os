using Emissor.Application.Security.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Security.Password;

public class PasswordHashingManager
{

    private readonly PasswordHashing hashing = new PasswordHashing(new BCryptPasswordHashStrategy()); 

    public string GenerateHash(string password) => hashing.Hash(password);

    public bool VerifyHash(string password, string hash) => hashing.Verify(password, hash); 

}

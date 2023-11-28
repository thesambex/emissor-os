using Emissor.Application.Security.Password;
using Emissor.Infra.Security.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Test.Password;

public class PasswordTest
{

    [Fact]
    public void Shoud_Hash_With_BCrypt_And_Return_True()
    {
        var hashing = new PasswordHashing(new BCryptPasswordHashStrategy());

        var password = "@Tes_t196$";
        var hash = hashing.Hash(password);
        Assert.True(hashing.Verify(password, hash));
    }

}

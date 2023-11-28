﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Security.Password;

public interface IPasswordHashStrategy
{
    string Hash(string password);
    bool Verify(string password, string hash);
}

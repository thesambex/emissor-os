# Emissor OS

Este projeto tem como propósito simular a emissão de ordens de serviço, nele foi focado mais o lado backend em .NET com C# utilizando ASP.NET core, Entity Framework, Docker, Postgresql e Arquitetura limpa com padrões de projetos.

- Exemplo de appsettings.json que deve ficar em emissor-os-backend/Emissor.API :
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "Postgresql": "Server=database;Port=5432;Username=postgres;Password=123456;Database=db_emissor_os"
  },
  "JwtSettings": {
    "Issuer": "http://localhost:7039",
    "Audience": "http://localhost:7039",
    "Key": "bmF0dGEuY29tL3JhaW94c2Nhbm5lcmoybWU="
  }
}
```
using Emissor.Application.Database;
using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Infra.Factory;
using Emissor.Infra.Repository;
using Emissor.Infra.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
    {
        opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Emissor OS", Version = "v1" });
    }
);

builder.Services.AddApiVersioning(opt =>
    {
        opt.DefaultApiVersion = new ApiVersion(1, 0);
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.ReportApiVersions = true;
    }
);

builder.Services.AddDbContext<PgContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql")));

builder.Services.AddTransient<IUnitOfWork, UnitOfWorkImpl>();
builder.Services.AddTransient<IAbstractRepositoryFactory, AbstractRepositoryFactoryImpl>();
builder.Services.AddScoped<IUsuariosService, UsuariosServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

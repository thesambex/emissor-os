using Emissor.Application.Database;
using Emissor.Application.Factory;
using Emissor.Application.Providers;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Infra.Auth;
using Emissor.Infra.Factory;
using Emissor.Infra.Repository;
using Emissor.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
    {
        opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Emissor OS", Version = "v1" });
        opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Por favor insira o token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });

        opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });
    }
);

builder.Services.AddApiVersioning(opt =>
    {
        opt.DefaultApiVersion = new ApiVersion(1, 0);
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.ReportApiVersions = true;
    }
);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
    };

});

builder.Services.AddAuthorization();

var CORSPOlicy = "_CORSPolicy";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: CORSPOlicy,
        policy =>
        {
            policy.WithOrigins("http://localhost", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddDbContext<PgContext>(opt => opt.UseNpgsql(config["ConnectionStrings:Postgresql"]));
builder.Services.AddTransient<IJwtProvider, JwtProviderImpl>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWorkImpl>();
builder.Services.AddTransient<IAbstractRepositoryFactory, AbstractRepositoryFactoryImpl>();
builder.Services.AddScoped<IUsuariosService, UsuariosServiceImpl>();
builder.Services.AddScoped<IAuthService, AuthServiceImpl>();
builder.Services.AddScoped<IClientesService, ClienteServiceImpl>();
builder.Services.AddScoped<IOrdemServicoService, OrdemServicoServiceImpl>();
builder.Services.AddScoped<IMercadoriasService, MercadoriasServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    using (var scope = app.Services.CreateScope())
    {
        var service = scope.ServiceProvider;
        var context = service.GetRequiredService<PgContext>();
        context.Database.Migrate();
    }
}

app.UseHttpsRedirection();
app.UseCors(CORSPOlicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

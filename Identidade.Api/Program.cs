using Identidade.Application.Gateways;
using Identidade.Application.UseCases;
using Identidade.Domain.Interfaces;
using Identidade.Infrastructure.ContextDb;
using Identidade.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<IdentidadeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Repositories
builder.Services.AddScoped<IdentidadeRepository>();

// Gateways
builder.Services.AddScoped<IIdentidadeGateway, IdentidadeGateway>();

// Use Cases
builder.Services.AddScoped<CriarClienteUseCase>();
builder.Services.AddScoped<ObterClientePorIdUseCase>();
builder.Services.AddScoped<ListarClientesUseCase>();
builder.Services.AddScoped<LoginClienteUseCase>();
builder.Services.AddScoped<LoginClienteAnonimoUseCase>();
builder.Services.AddScoped<AlterarStatusClienteUseCase>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "MinhaChaveSecretaSuperSegura123456789"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identidade API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "Identidade API";
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<IdentidadeContext>();
    context.Database.Migrate();
}

app.Run();

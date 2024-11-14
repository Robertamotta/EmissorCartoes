using EmissorCartoes.Api.Consumers;
using EmissorCartoes.Dominio.Interfaces;
using EmissorCartoes.Dominio.Servico;
using EmissorCartoes.Infraestrutura.Servico.Mensageria;

var builder = WebApplication.CreateBuilder(args);

// Dependency Resolver 
builder.Services.AddScoped<IMensageria, Mensageria>();
builder.Services.AddScoped<IProcessadorEmissaoCartaoServico, ProcessadorEmissaoCartaoServico>();
builder.Services.AddHostedService<PropostaCreditoConsumer>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


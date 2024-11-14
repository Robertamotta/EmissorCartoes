using Newtonsoft.Json;
using EmissorCartoes.Dominio.DTOs;
using EmissorCartoes.Dominio.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace EmissorCartoes.Api.Consumers;

[ExcludeFromCodeCoverage]
public class PropostaCreditoConsumer : BackgroundService
{
    private readonly IConnection connection;
    private readonly IModel channel;
    private readonly IServiceProvider services;
    private readonly ILogger<PropostaCreditoConsumer> logger;

    private const string Queue = "queue.emissaocartaocredito.v1";

    public PropostaCreditoConsumer(ILogger<PropostaCreditoConsumer> logger, IServiceProvider services)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
        };

        connection = connectionFactory.CreateConnection();
        
        channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: Queue,
            durable: false,
            exclusive: false,
            autoDelete: false);

        this.logger = logger;
        this.services = services;

    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();

            var contentString = Encoding.UTF8.GetString(contentArray);
            var propostaCredito = JsonConvert.DeserializeObject<PropostaCredito>(contentString);

            if (propostaCredito == null)
            {
                logger.LogError("Proposta de credito recebida é nula ou inválida");
                return;
            }

            await Complete(propostaCredito);

            channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        channel.BasicConsume(Queue, false, consumer);

        return Task.CompletedTask;
    }

    public async Task Complete(PropostaCredito propostaCredito)
    {
        using var scope = services.CreateScope();

        var processadorEmissaoCartaoServico = scope.ServiceProvider.GetRequiredService<IProcessadorEmissaoCartaoServico>();

        await processadorEmissaoCartaoServico.Processar(propostaCredito);
    }
}

using EmissorCartoes.Dominio.DTOs;
using EmissorCartoes.Dominio.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace EmissorCartoes.Infraestrutura.Servico.Mensageria;
public class Mensageria(ILogger<Mensageria> logger) : IMensageria
{
    public Task EnviarEmissaoCartoes(EmissaoCartaoCreditoDto emissaoCartao)
    {
        try
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var queue = "queue.respostaemissaocartaocredito.v1";
            channel.QueueDeclare(queue, false, false, false, null);

            channel.BasicPublish(exchange: string.Empty, routingKey: queue, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(emissaoCartao)));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ocorreu um erro ao postar a mensagem na fila de emissao de cartões");
        }

        return Task.CompletedTask;
    }
}

using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gerador_De_Certificados.Controllers{
    public class RabbitMqSender
    {
        private const string QueueName = "diplomasQueue";
        private const string RabbitMqUri = "amqp://rabbitmq";

        public async Task SendToQueueAsync(object message)
        {
            try
            {
                var factory = new ConnectionFactory() { Uri = new Uri(RabbitMqUri) };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: properties, body: messageBody);

                Console.WriteLine("Mensagem enviada para fila: " + JsonSerializer.Serialize(message));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Erro ao enviar mensagem para fila: " + ex.Message);
            }
        }
    }
}

using EmailMessageConsumer.Domain.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace EmailMessageConsumer
{
    public class EmailMessageSubscriber : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string _queue = "userapi-email-service-queue";

        public EmailMessageSubscriber()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = Environment.GetEnvironmentVariable("RABBIT_MQ_URL")
            };

            _connection = connectionFactory.CreateConnection("userapi-provider-name");

            _channel = _connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var emailMessage = JsonSerializer.Deserialize<EmailMessage>(contentString);

                if(emailMessage != null)
                {
                    Console.WriteLine(emailMessage.ToString());
                }

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(_queue, false, consumer);

            return Task.CompletedTask;
        }
    }
}

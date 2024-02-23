using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace UserAPI.Application.Services.Message
{
    public class EmailMessengerService : IEmailMessengerService
    {
        private IConnection? _connection;
        private IModel? _channel;
        private const string Exchange = "userapi-service-exchange";
        private const string RountingKey = "userapi-routing-key";
        private const string ConnectionName = "userapi-service-publisher";
        private readonly ILogger<EmailMessengerService> _logger;

        public EmailMessengerService(ILogger<EmailMessengerService> logger)
        {
            _logger = logger;
            ConnectToMessageBroker();
        }

        public void SendMessage(Email email)
        {
            try
            {
                var payload = JsonSerializer.Serialize(email);
                var byteArray = Encoding.UTF8.GetBytes(payload);

                _channel.BasicPublish(Exchange, RountingKey, null, byteArray);

                _logger.LogInformation("The message was send successfully. @payload", payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to send the message.");
            }
        }

        private void ConnectToMessageBroker()
        {
            try
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = Environment.GetEnvironmentVariable("RABBIT_MQ_URL")
                };

                _connection = connectionFactory.CreateConnection(ConnectionName);

                _channel = _connection.CreateModel();

                _logger.LogInformation("The connection with RabbitMQ was done successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to connect to RabbitMQ.");
            }
        }
    }
}

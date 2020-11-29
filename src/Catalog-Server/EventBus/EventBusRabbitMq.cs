using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Catalog_Server.EventBus
{
    public class EventBusRabbitMq<T> : IEventBus<T> where T : BaseMessage
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly ILogger<EventBusRabbitMq<T>> logger;
        private readonly ExchangeBinding exchangeBinding;
        private readonly JsonSerializerSettings jsonSerializerSettings;

        public EventBusRabbitMq(ConnectionFactory connectionFactory, ILogger<EventBusRabbitMq<T>> logger,
            ExchangeBinding exchangeBinding,
            JsonSerializerSettings jsonSerializerSettings)
        {
            this.connectionFactory = connectionFactory;
            this.logger = logger;
            this.exchangeBinding = exchangeBinding;
            this.jsonSerializerSettings = jsonSerializerSettings;
        }

        public Task Publish(T message)
        {
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            var @event = JsonConvert.SerializeObject(message, jsonSerializerSettings);
            var bytes = Encoding.UTF8.GetBytes(@event);
            logger.LogInformation($"Publishing event to RabbitMQ: {message.EventId}");
            channel?.BasicPublish(exchange: exchangeBinding.Exchange,
                routingKey: exchangeBinding.RoutingKey,
                body: bytes,
                basicProperties: null);
            return Task.CompletedTask;
        }
    }
}
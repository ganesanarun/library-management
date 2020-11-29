using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Search_Server.Books;

namespace Search_Server
{
    public class EventConsumer
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly ILogger<EventConsumer> logger;
        private readonly BookChangeEventHandler handler;
        private readonly JsonSerializerSettings serializerSettings;
        private readonly string queue;

        public EventConsumer(ConnectionFactory connectionFactory,
            ILogger<EventConsumer> logger,
            BookChangeEventHandler handler,
            JsonSerializerSettings serializerSettings,
            IOptions<QueueOptions> queue)
        {
            this.connectionFactory = connectionFactory;
            this.logger = logger;
            this.handler = handler;
            this.serializerSettings = serializerSettings;
            this.queue = queue.Value.Queue;
        }

        public Task Start()
        {
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            channel.BasicQos(0, 3, false);

            var asyncEventingBasicConsumer = new EventingBasicConsumer(channel);
            asyncEventingBasicConsumer.Received += async (sender, @event) =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(@event.Body.ToArray());
                    var bookChangeEvent = JsonConvert.DeserializeObject<BookChangeEvent>(message, serializerSettings);
                    logger.LogInformation($"Received book change event {bookChangeEvent.EventId}");
                    await handler.Handle(bookChangeEvent);
                    channel.BasicAck(@event.DeliveryTag, false);
                }
                catch (Exception e)
                {
                    logger.LogError($"Failed with an exception {e}");
                }
            };
            channel.BasicConsume(queue, autoAck: false, consumer: asyncEventingBasicConsumer);
            return Task.CompletedTask;
        }
    }
}
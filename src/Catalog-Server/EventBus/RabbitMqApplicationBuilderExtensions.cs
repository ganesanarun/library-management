using System;
using Catalog_Server.books;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;

namespace Catalog_Server.EventBus
{
    public static class RabbitMqApplicationBuilderExtensions
    {
        public static void AddRabbitMq(this IServiceCollection services, in IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("Catalog:RabbitMq:ConnectionUrl")?.Value ??
                                   "amqp://guest:guest@localhost:5672/";
            var exchange = configuration.GetSection("Catalog:RabbitMq:Exchange")?.Value ?? "library.books";
            var routingKey = configuration.GetSection("Catalog:RabbitMq:RoutingKey")?.Value ?? "change";
            services.AddSingleton(new ConnectionFactory
                {
                    Endpoint = new AmqpTcpEndpoint(new Uri(connectionString))
                })
                .AddSingleton(new ExchangeBinding(exchange, routingKey));
            services.AddSingleton(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            services.AddSingleton<IEventBus<BookChangeEvent>, EventBusRabbitMq<BookChangeEvent>>();
        }
    }
}
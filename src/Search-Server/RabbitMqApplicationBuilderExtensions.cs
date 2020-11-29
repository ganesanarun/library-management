using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;

namespace Search_Server
{
    public static class RabbitMqApplicationBuilderExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, in IConfiguration configuration)
        {
            services.Configure<QueueOptions>(configuration.GetSection(QueueOptions.QueueSection));
            var connectionString = configuration.GetSection(QueueOptions.ConnectionUrlSection)?.Value ??
                                   "amqp://guest:guest@localhost:5672/";
            services.AddSingleton(new ConnectionFactory
            {
                Endpoint = new AmqpTcpEndpoint(new Uri(connectionString))
            });
            services.AddSingleton(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            return services;
        }
    }
}
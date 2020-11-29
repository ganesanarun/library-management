using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Search_Server.ElasticSearch
{
    public static class ApplicationBuilderExtensions
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services,
            in IConfiguration configuration)
        {
            var elasticUrl = configuration.GetSection("Search:Elastic:Url")?.Value;
            var node = new Uri(elasticUrl);
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);
            services.AddSingleton(client);
            return services;
        }
    }
}
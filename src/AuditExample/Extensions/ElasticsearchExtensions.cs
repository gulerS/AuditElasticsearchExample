using System;
using AuditExample.ElasticSearch;
using AuditExample.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace AuditExample.Extensions
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ElasticSearch:URL"];
            var defaultIndex = configuration["ElasticSearch:Index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .MapDefaultTypeIndices(m => m.Add(typeof(ChangeLog), defaultIndex));

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
            services.AddScoped<IElasticSearch, ElasticSearch.ElasticSearch>();
        }
    }
}
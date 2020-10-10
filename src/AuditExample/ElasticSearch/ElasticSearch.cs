using System;
using AuditExample.Models;
using Microsoft.Extensions.Configuration;
using Nest;

namespace AuditExample.ElasticSearch
{
    public class ElasticSearch : IElasticSearch
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearch(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public void CheckExistsAndInsert(ChangeLog log)
        {
            //elasticClient.DeleteIndex("change_log");         
            if (!_elasticClient.IndexExists("change_log").Exists)
            {
                var indexSettings = new IndexSettings();
                indexSettings.NumberOfReplicas = 1;
                indexSettings.NumberOfShards = 3;


                var createIndexDescriptor = new CreateIndexDescriptor("change_history")
                    .Mappings(ms => ms
                        .Map<ChangeLog>(m => m.AutoMap())
                    )
                    .InitializeUsing(new IndexState() {Settings = indexSettings})
                    .Aliases(a => a.Alias("change_log"));

                var response = _elasticClient.CreateIndex(createIndexDescriptor);
            }

            _elasticClient.Index<ChangeLog>(log, idx => idx.Index("change_history"));
        }
    }
}
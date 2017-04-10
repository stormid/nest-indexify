using System;

namespace Nest.Indexify.Contributors.PreCreate
{
    public class IndexAliasingContributor : ElasticsearchIndexContributor, IElasticsearchIndexPreCreationContributor, IElasticsearchIndexCreationSuccessContributor
    {
        private string _indexName;

        public string OnPreCreate(IElasticClient client, string indexName)
        {
            _indexName = $"{indexName}-{DateTime.UtcNow.Ticks}".ToLowerInvariant();
            return _indexName;
        }

        public void OnSuccess(IElasticClient client, ICreateIndexResponse response)
        {
            client.Alias(a => a.Add(aa => aa.Alias(client.ConnectionSettings.DefaultIndex).Index(_indexName)));
        }
    }
}

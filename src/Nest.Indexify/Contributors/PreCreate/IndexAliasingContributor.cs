using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void OnSuccess(IElasticClient client, IIndicesOperationResponse response)
        {
            client.Alias(a => a.Add(aa => aa.Alias(client.Infer.DefaultIndex).Index(_indexName)));
        }
    }
}

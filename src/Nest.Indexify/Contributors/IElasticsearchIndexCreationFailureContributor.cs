using System;

namespace Nest.Indexify.Contributors
{
    public interface IElasticsearchIndexCreationFailureContributor : IElasticsearchIndexContributor
    {
        void OnFailure(IElasticClient client, IIndicesOperationResponse response = null, Exception ex = null);
    }
}
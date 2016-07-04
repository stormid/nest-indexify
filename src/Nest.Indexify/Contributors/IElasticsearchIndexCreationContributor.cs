using System;

namespace Nest.Indexify.Contributors
{
    public interface IElasticsearchIndexCreationContributor : IElasticsearchIndexContributor
    {
        void Contribute(CreateIndexDescriptor descriptor, IElasticClient client);
    }
}
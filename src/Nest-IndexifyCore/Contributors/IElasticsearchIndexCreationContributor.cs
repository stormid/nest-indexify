using System;

namespace Nest.Indexify.Contributors
{
    public interface IElasticsearchIndexSettingsContributor : IElasticsearchIndexContributor
    {
        void Contribute(IndexSettingsDescriptor descriptor, IElasticClient client);
    }

    public interface IElasticsearchIndexCreationContributor : IElasticsearchIndexContributor
    {
        void Contribute(CreateIndexDescriptor descriptor, IElasticClient client);
    }
}
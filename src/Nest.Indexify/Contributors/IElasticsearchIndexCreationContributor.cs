using System;

namespace Nest.Indexify.Contributors
{
    public interface IElasticsearchIndexAnalysisContributor : IElasticsearchIndexContributor
    {
        void Contribute(AnalysisDescriptor descriptor, IElasticClient client);
    }

    public interface IElasticsearchIndexSettingsContributor : IElasticsearchIndexContributor
    {
        void Contribute(IndexSettingsDescriptor descriptor, IElasticClient client);
    }

    public interface IElasticsearchIndexCreationContributor : IElasticsearchIndexContributor
    {
        void Contribute(CreateIndexDescriptor descriptor, IElasticClient client);
    }
}
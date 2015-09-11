using System;

namespace Nest.Indexify.Contributors
{
    public interface IElasticsearchIndexContributor : IComparable<IElasticsearchIndexContributor>
    {
        int Order { get; }
    }

    public interface IElasticsearchIndexCreationContributor : IElasticsearchIndexContributor, IComparable<IElasticsearchIndexCreationContributor>
    {
        void Contribute(CreateIndexDescriptor descriptor, IElasticClient client);
    }

    public interface IElasticsearchIndexCreationSuccessContributor : IElasticsearchIndexContributor
    {
        void OnSuccess(IElasticClient client, IIndicesOperationResponse response);
    }

    public interface IElasticsearchIndexCreationFailureContributor : IElasticsearchIndexContributor
    {
        void OnFailure(IElasticClient client, Exception ex);
    }

    public interface IElasticsearchIndexPreCreationContributor : IElasticsearchIndexContributor
    {
        string OnPreCreate(IElasticClient client, string indexName);
    }
}
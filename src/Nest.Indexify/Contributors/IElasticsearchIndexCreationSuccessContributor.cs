namespace Nest.Indexify.Contributors
{
    public interface IElasticsearchIndexCreationSuccessContributor : IElasticsearchIndexContributor
    {
        void OnSuccess(IElasticClient client, IIndicesOperationResponse response);
    }
}
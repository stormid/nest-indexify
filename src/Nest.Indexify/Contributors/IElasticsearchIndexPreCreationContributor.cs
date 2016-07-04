namespace Nest.Indexify.Contributors
{
    public interface IElasticsearchIndexPreCreationContributor : IElasticsearchIndexContributor
    {
        string OnPreCreate(IElasticClient client, string indexName);
    }
}
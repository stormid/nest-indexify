using Nest.Indexify.Contributors;

namespace Nest.Indexify.Tests
{
    public class StubElasticsearchIndexCreationStrategy : ElasticsearchIndexCreationStrategy
    {
        public StubElasticsearchIndexCreationStrategy(IElasticClient client, params IElasticsearchIndexCreationContributor[] contributors) : base(client)
        {
            foreach (var c in contributors)
            {
                AddContributor(c);
            }
        }
    }
}
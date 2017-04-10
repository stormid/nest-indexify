using Nest.Indexify.Tests.IndexCreationContributorSpecs;
using Xunit;

namespace Nest.Indexify.Tests
{
    public abstract class IndexCreationStrategyContext : IClassFixture<ElasticClientQueryObjectTestFixture>
    {
        protected readonly ElasticClientQueryObjectTestFixture Fixture;

        protected IndexCreationStrategyContext(ElasticClientQueryObjectTestFixture fixture)
        {
            Fixture = fixture;
        }
    }
}
using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.IndexSettings;
using Nest.Indexify.Tests.IndexCreationContributorSpecs;
using Nest.Indexify.Tests.Stubs;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public abstract class AddAliasIndexCreationContributorSpecs
    {
        public class WhenAddingAliasContributor : IndexCreationStrategyContext
        {
            [Fact]
            public void ShouldAddAlias()
            {
                var contributor = new AddAliasIndexSettingsContributor("alias");
                var strategy = new StubElasticsearchIndexCreationStrategy(Fixture.Client, contributor);
                var result = strategy.Create();
                var details = Fixture.DeserializeRequestBody<ICreateIndexRequest>(result.IndexResponse.ApiCall);
                details.Aliases.ContainsKey("alias").Should().BeTrue();
            }

            public WhenAddingAliasContributor(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }
        }
    }
}
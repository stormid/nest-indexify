using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Tests.IndexCreationContributorSpecs;
using Nest.Indexify.Tests.Stubs;
using Xunit;

namespace Nest.Indexify.Tests
{
    public class ElasticsearchIndexCreationStrategySpecs : IClassFixture<ElasticClientQueryObjectTestFixture>
    {
        private readonly ElasticClientQueryObjectTestFixture fixture;

        public ElasticsearchIndexCreationStrategySpecs(ElasticClientQueryObjectTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void ShouldCallElasticClientCreateIndex()
        {
            IElasticsearchIndexContributor[] stubContributors = new List<IElasticsearchIndexCreationContributor>
            {
                new StubElasticsearchIndexCreationContributor(true, 3),
                new StubElasticsearchIndexCreationContributor(true, 2),
                new StubElasticsearchIndexCreationContributor(true, 1)
            }.ToArray();
            var strategy = new StubElasticsearchIndexCreationStrategy(fixture.Client, stubContributors);

            var result = strategy.Create();
            result.IndexResponse.ApiCall.Success.Should().BeTrue();
            result.IndexResponse.ApiCall.HttpStatusCode.Should().Be(200);
            result.IndexResponse.ApiCall.HttpMethod.Should().Be(HttpMethod.PUT);
            result.Contributors.Count().Should().Be(3);
        }

        [Fact]
        public void ShouldHaveCalledAllContributors()
        {
            IElasticsearchIndexContributor[] stubContributors = new List<IElasticsearchIndexCreationContributor>
            {
                new StubElasticsearchIndexCreationContributor(true, 3),
                new StubElasticsearchIndexCreationContributor(true, 2),
                new StubElasticsearchIndexCreationContributor(true, 1)
            }.ToArray();
            var strategy = new StubElasticsearchIndexCreationStrategy(fixture.Client, stubContributors);

            var result = strategy.Create();
            stubContributors
                .OfType<StubElasticsearchIndexCreationContributor>()
                .All(x => x.HasContributed)
                .Should().BeTrue();
        }

        [Fact]
        public void ShouldReportContributions()
        {
            IElasticsearchIndexContributor[] stubContributors = new List<IElasticsearchIndexCreationContributor>
            {
                new StubElasticsearchIndexCreationContributor(true, 3),
                new StubElasticsearchIndexCreationContributor(false, 2),
                new StubElasticsearchIndexCreationContributor(true, 1)
            }.ToArray();
            var strategy = new StubElasticsearchIndexCreationStrategy(fixture.Client, stubContributors);

            var result = strategy.Create();
            result.Contributors.Count(x => x.HasContributed).Should().Be(2);
        }
    }
}
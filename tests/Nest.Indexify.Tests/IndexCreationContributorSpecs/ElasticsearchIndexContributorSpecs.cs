using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Tests.Stubs;
using Xunit;

namespace Nest.Indexify.Tests.IndexCreationContributorSpecs
{
    public abstract class ElasticsearchIndexContributorSpecs : IClassFixture<ElasticClientQueryObjectTestFixture>
    {
        protected readonly ElasticClientQueryObjectTestFixture Fixture;
        protected ElasticsearchIndexContributorSpecs(ElasticClientQueryObjectTestFixture fixture)
        {
            Fixture = fixture;
        }
    }

    public class ElasticsearchIndexStrategyCreate : IClassFixture<ElasticClientQueryObjectTestFixture>
    {
        private readonly ElasticClientQueryObjectTestFixture _fixture;

        public ElasticsearchIndexStrategyCreate(ElasticClientQueryObjectTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CreatingIndex()
        {
            var strategy = new StubElasticsearchIndexCreationStrategy(_fixture.Client);
            var result = strategy.Create();
            _fixture.ShouldUseHttpMethod("PUT");
            result.Success.Should().BeTrue();
            result.Contributors.All(x => x.HasContributed).Should().BeTrue();
        }

        [Fact]
        public async void CreatingIndexAsync()
        {
            var strategy = new StubElasticsearchIndexCreationStrategy(_fixture.Client);
            var result = await strategy.CreateAsync();
            _fixture.ShouldUseHttpMethod("PUT");
            result.Success.Should().BeTrue();
            result.Contributors.All(x => x.HasContributed).Should().BeTrue();
        }
    }

    public class ElasticsearchIndexStrategyCreateAsync : IClassFixture<ElasticClientQueryObjectTestFixture>
    {
        private readonly ElasticClientQueryObjectTestFixture _fixture;

        public ElasticsearchIndexStrategyCreateAsync(ElasticClientQueryObjectTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void CreatingIndexAsync()
        {
            var strategy = new StubElasticsearchIndexCreationStrategy(_fixture.Client);
            var result = await strategy.CreateAsync();
            _fixture.ShouldUseHttpMethod("PUT");
            result.Success.Should().BeTrue();
            result.Contributors.All(x => x.HasContributed).Should().BeTrue();

        }
    }

    public class ElasticsearchIndexStrategyCreateSettings : IClassFixture<ElasticClientQueryObjectTestFixture>
    {
        private readonly ElasticClientQueryObjectTestFixture _fixture;

        public ElasticsearchIndexStrategyCreateSettings(ElasticClientQueryObjectTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CreatingIndex()
        {
            var strategy = new StubElasticsearchIndexCreationStrategy(_fixture.Client);
            var result = strategy.Create();
            result.Success.Should().BeTrue();
        }
    }

    public class OrderingElasticsearchIndexContributors : ElasticsearchIndexContributorSpecs
    {
        public class StubIndexContributor : ElasticsearchIndexContributor
        {
            public StubIndexContributor(int order) : base(order) { }
        }

        public OrderingElasticsearchIndexContributors(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void ShouldMaintainOrderOfContributors()
        {
            var contribs = new SortedSet<IElasticsearchIndexContributor>
            {
                new StubIndexContributor(3),
                new StubIndexContributor(2),
                new StubIndexContributor(4),
                new StubIndexContributor(5),
                new StubIndexContributor(1)
            };

            contribs.Should().BeInAscendingOrder(x => x.Order);
        }
    }
}

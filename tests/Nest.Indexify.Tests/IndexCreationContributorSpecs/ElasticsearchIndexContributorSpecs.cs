using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Moq;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.IndexSettings;
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
            _fixture.ShouldUseHttpMethod("POST");
            result.Success.Should().BeTrue();
            result.Contributors.All(x => x.HasContributed).Should().BeTrue();
        }

        [Fact]
        public async void CreatingIndexAsync()
        {
            var strategy = new StubElasticsearchIndexCreationStrategy(_fixture.Client);
            var result = await strategy.CreateAsync();
            _fixture.ShouldUseHttpMethod("POST");
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
            _fixture.ShouldUseHttpMethod("POST");
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
            strategy.Create();
            var response = _fixture.RespondsWith<IndexSettings>();
            response.Should().NotBeNull();
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

            contribs.ElementAt(0).Order.Should().Be(1);
            contribs.ElementAt(1).Order.Should().Be(2);
            contribs.ElementAt(2).Order.Should().Be(3);
            contribs.ElementAt(3).Order.Should().Be(4);
            contribs.ElementAt(4).Order.Should().Be(5);
        }
    }
}

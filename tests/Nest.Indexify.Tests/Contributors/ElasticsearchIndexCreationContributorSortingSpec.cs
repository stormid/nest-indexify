using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Tests.IndexCreationContributorSpecs;
using Nest.Indexify.Tests.Stubs;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public class ElasticsearchIndexCreationContributorSortingSpec : IClassFixture<ElasticsearchIndexCreationContributorSortingSpec.IndexCreationContributorsClassFixture>
    {
        private readonly IndexCreationContributorsClassFixture classFixture;

        public class IndexCreationContributorsClassFixture : ElasticClientQueryObjectTestFixture
        {
            public IEnumerable<IElasticsearchIndexCreationContributor> Contributors { get; private set; }

            public IndexCreationContributorsClassFixture()
            {
                Contributors = new List<IElasticsearchIndexCreationContributor>()
                {
                    new StubElasticsearchIndexCreationContributor(true, 10),
                    new StubElasticsearchIndexCreationContributor(true, 2),
                    new StubElasticsearchIndexCreationContributor(true, 0),
                    new StubElasticsearchIndexCreationContributor(true, 0),
                    new StubElasticsearchIndexCreationContributor(true, 99),
                    new StubElasticsearchIndexCreationContributor(true, 50)
                };
            }

            public override void Dispose()
            {
                Contributors = Enumerable.Empty<IElasticsearchIndexCreationContributor>();
                base.Dispose();
            }
        }

        public ElasticsearchIndexCreationContributorSortingSpec(IndexCreationContributorsClassFixture classFixture)
        {
            this.classFixture = classFixture;
        }

        [Fact]
        public void ShouldAddAllContributors()
        {
            var strategy = new StubElasticsearchIndexCreationStrategy(classFixture.Client, classFixture.Contributors.ToArray());
            strategy.Create();
            strategy.Contributors.Count().Should().Be(classFixture.Contributors.Count());
        }

        [Fact]
        public void ShouldSortContributorsByOrder()
        {
            var strategy = new StubElasticsearchIndexCreationStrategy(classFixture.Client, classFixture.Contributors.ToArray());
            var result = strategy.Create();
            result.Contributors.Should().BeInAscendingOrder(c => c.Order);
        }
    }
}

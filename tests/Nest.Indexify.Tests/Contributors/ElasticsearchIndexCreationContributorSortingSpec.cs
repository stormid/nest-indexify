using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Tests.Specification;
using Nest.Indexify.Tests.Stubs;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public class ElasticsearchIndexCreationContributorSortingSpec : ContextSpecification<ElasticsearchIndexCreationContributorSortingSpec.IndexCreationContributorsContext>
    {
        public class IndexCreationContributorsContext : IDisposable
        {
            public IEnumerable<IElasticsearchIndexCreationContributor> Contributors { get; private set; }

            public IndexCreationContributorsContext()
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

            public void Dispose()
            {
                Contributors = Enumerable.Empty<IElasticsearchIndexCreationContributor>();
            }
        }

        private readonly ISet<IElasticsearchIndexCreationContributor> _contributors = new SortedSet<IElasticsearchIndexCreationContributor>();

        public ElasticsearchIndexCreationContributorSortingSpec(IndexCreationContributorsContext context) : base(context)
        {
        }

        protected override void Because()
        {
            foreach (var c in SharedContext.Contributors)
            {
                _contributors.Add(c);
            }
        }

        [Fact]
        public void ShouldAddAllContributors()
        {
            _contributors.Should().HaveCount(SharedContext.Contributors.Count());
        }

        [Fact]
        public void ShouldSortContributorsByOrder()
        {
            _contributors.Should().BeInAscendingOrder(c => c.Order);
        }
    }
}

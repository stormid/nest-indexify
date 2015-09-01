using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using Nest.Indexify.Contributors;
using Nest.Indexify.Tests.Specification;
using Xunit;

namespace Nest.Indexify.Tests
{
    public class ElasticsearchIndexCreationStrategySpecs : ContextSpecification<ElasticClientContext>
    {
        private IElasticsearchIndexCreationStrategy _strategy;

        private IEnumerable<IElasticsearchIndexCreationContributor> _stubContributors;
        
        protected override void Context()
        {
            _stubContributors = new[]
            {
                new StubElasticsearchIndexCreationContributor(3),
                new StubElasticsearchIndexCreationContributor(2),
                new StubElasticsearchIndexCreationContributor(1)
            };
            _strategy = new StubElasticsearchIndexCreationStrategy(SharedContext.Client.Object, _stubContributors.ToArray());
        }

        protected override void Because()
        {
            _strategy.Create();
        }

        [Fact]
        public void ShouldCallElasticClientCreateIndex()
        {
            SharedContext.Client.Verify(v => v.CreateIndex(It.IsAny<Func<CreateIndexDescriptor, CreateIndexDescriptor>>()), Times.Once);
        }

        [Fact]
        public void ShouldHaveCalledAllContributors()
        {
            _stubContributors.OfType<StubElasticsearchIndexCreationContributor>()
                .All(x => x.HasContributed)
                .Should()
                .BeTrue();
        }
    }
}
using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Tests.Stubs;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public class WhenIndexCreationContributionCanContribute : IndexCreationStrategyContext
    {
        private StubElasticsearchIndexCreationContributor _contributor;

        protected override void Context()
        {
            _contributor = new StubElasticsearchIndexCreationContributor(true);
            base.Context();
        }

        protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
        {
            yield return _contributor;
        }

        [Fact]
        public void ShouldNotContributeToIndexCreation()
        {
            _contributor.HasContributed.Should().BeTrue();
        }
    }
}
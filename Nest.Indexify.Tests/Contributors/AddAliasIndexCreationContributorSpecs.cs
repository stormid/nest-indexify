using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.IndexSettings;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public abstract class AddAliasIndexCreationContributorSpecs
    {
        public class WhenAddingAliasContributor : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new AddAliasIndexSettingsContributor("alias");
            }

            [Fact]
            public void ShouldAddAlias()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Aliases.ContainsKey("alias").Should().BeTrue();
            }
        }
    }
}
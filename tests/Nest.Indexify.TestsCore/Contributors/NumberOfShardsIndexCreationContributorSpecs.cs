using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.IndexSettings;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public abstract class NumberOfShardsIndexCreationContributorSpecs
    {
        public class WhenAddingShardsContributorWithValue : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new NumberOfShardsIndexSettingsContributor(3);
            }

            [Fact]
            public void ShouldConfigureNumberOfShards()
            {
                SharedContext.IndexCreateRequest.Settings.NumberOfShards.Should().HaveValue().And.Be(3);
            }
        }

        public class WhenAddingShardsContributorWithoutValue : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new NumberOfShardsIndexSettingsContributor();
            }

            [Fact]
            public void ShouldConfigureNumberOfShards()
            {
                SharedContext.IndexCreateRequest.Settings.NumberOfShards.Should().NotHaveValue();
            }
        }
    }
}
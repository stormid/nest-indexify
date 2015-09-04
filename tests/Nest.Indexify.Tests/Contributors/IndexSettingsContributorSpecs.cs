using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.IndexSettings;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public abstract class IndexSettingsContributorSpecs
    {
        public class WhenAddDefaultIndexSettings : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new IndexSettingsContributor();
            }

            [Fact]
            public void ShouldIgnoreShardsSetting()
            {
                SharedContext.IndexCreateRequest.IndexSettings.NumberOfShards.Should().NotHaveValue();
            }

            [Fact]
            public void ShouldIgnoreReplicasSetting()
            {
                SharedContext.IndexCreateRequest.IndexSettings.NumberOfReplicas.Should().NotHaveValue();
            }
        }

        public class WhenAddDefaultIndexSettingsWithValues : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new IndexSettingsContributor(3, 2);
            }

            [Fact]
            public void ShouldIgnoreShardsSetting()
            {
                SharedContext.IndexCreateRequest.IndexSettings.NumberOfShards.Should().HaveValue().And.Be(3);
            }

            [Fact]
            public void ShouldIgnoreReplicasSetting()
            {
                SharedContext.IndexCreateRequest.IndexSettings.NumberOfReplicas.Should().HaveValue().And.Be(2);
            }
        }
    }
}
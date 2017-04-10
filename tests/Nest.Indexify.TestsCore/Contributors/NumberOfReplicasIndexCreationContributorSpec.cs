using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.IndexSettings;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public abstract class NumberOfReplicasIndexCreationContributorSpec
    {
        public class WhenAddingReplicasContributorWithValue : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new NumberOfReplicasIndexSettingsContributor(3);
            }

            [Fact]
            public void ShouldConfigureNumberOfReplicas()
            {
                SharedContext.IndexCreateRequest.Settings.NumberOfReplicas.Should().HaveValue().And.Be(3);
            }
        }

        public class WhenAddingReplicasContributorWithoutValue : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new NumberOfReplicasIndexSettingsContributor();
            }

            [Fact]
            public void ShouldConfigureNumberOfReplicas()
            {
                SharedContext.IndexCreateRequest.Settings.NumberOfReplicas.Should().NotHaveValue();
            }
        }
    }
}
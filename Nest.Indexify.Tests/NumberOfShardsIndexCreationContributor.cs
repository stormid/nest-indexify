using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.IndexSettings;
using Xunit;

namespace Nest.Indexify.Tests
{
    public class NumberOfShardsIndexCreationContributor : IndexCreationStrategyContext
    {
        protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
        {
            yield return new NumberOfShardsIndexSettingsContributor(3);
        }
        
        [Fact]
        public void ShouldConfigureNumberOfShards()
        {
            SharedContext.IndexCreateRequest.IndexSettings.NumberOfShards.Should().Be(3);
        }
    }
}
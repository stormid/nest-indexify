using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.Autocomplete;
using Nest.Indexify.Contributors.IndexSettings;
using Xunit;

namespace Nest.Indexify.Tests
{
    public class NumberOfReplicasIndexCreationContributorSpec : IndexCreationStrategyContext
    {
        protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
        {
            yield return new NumberOfReplicasIndexSettingsContributor(3);
        }

        [Fact]
        public void ShouldConfigureNumberOfReplicas()
        {
            SharedContext.IndexCreateRequest.IndexSettings.NumberOfReplicas.Should().Be(3);
        }
    }

    public class AutocompleteAnalysisIndexCreationContributorSpecs : IndexCreationStrategyContext
    {
        protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
        {
            yield return new AutoCompleteIndexAnalysisContributor("autocomplete", "edgengram");
        }

        [Fact]
        public void ShouldConfigureAnalyzer()
        {
            SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers.Should().ContainKey("autocomplete");
        }

        public void ShouldConfigureTokenFilter()
        {
            SharedContext.IndexCreateRequest.IndexSettings.Analysis.TokenFilters.Should().ContainKey("edgengram");
        }
    }

    public class WhenAddingAutocompleteAnalyzerWithoutTokenFilter : IndexCreationStrategyContext
    {
        protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
        {
            yield return new AutoCompleteAnalyzerContributor("autocomplete", "blah");
        }

        [Fact]
        public void ShouldNotConfigureAnalyzer()
        {
            SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers.Should().NotContainKey("autocomplete");
        }
    }
}
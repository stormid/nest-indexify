using System.Collections.Generic;
using Nest.Indexify.Contributors;
using Xunit;

namespace Nest.Indexify.Tests.Contributors.Analysis
{
    public abstract class AutocompleteAnalysisIndexCreationContributorSpecs
    {
        public class WhenAddingAnalysisWithNames : IndexCreationStrategyContext
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

            [Fact]
            public void ShouldConfigureTokenFilter()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.TokenFilters.Should().ContainKey("edgengram");
            }
        }

        public class WhenAddingAnalysisWithoutNames : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new AutoCompleteIndexAnalysisContributor();
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers.Should().ContainKey("indexify_ac");
            }

            [Fact]
            public void ShouldConfigureTokenFilter()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.TokenFilters.Should().ContainKey("indexify_ac");
            }
        }
    }
}
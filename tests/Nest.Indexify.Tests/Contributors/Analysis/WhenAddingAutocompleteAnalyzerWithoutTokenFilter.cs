using System.Collections.Generic;
using Nest.Indexify.Contributors;
using Xunit;

namespace Nest.Indexify.Tests.Contributors.Analysis
{
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
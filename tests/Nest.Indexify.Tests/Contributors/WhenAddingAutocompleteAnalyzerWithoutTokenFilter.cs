using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.Analysis.Autocomplete;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
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
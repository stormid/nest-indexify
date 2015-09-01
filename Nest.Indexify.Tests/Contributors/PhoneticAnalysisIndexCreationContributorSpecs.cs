using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.Analysis.Phonetic;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public abstract class PhoneticAnalysisIndexCreationContributorSpecs
    {
        public class WhenAddingPhoneticContributorWithName : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new PhoneticIndexAnalysisContributor("phonetic");
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers.Should().ContainKey("phonetic");
            }

            [Fact]
            public void ShouldConfigureTokenFilter()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.TokenFilters.Should().ContainKey("phonetic");
            }
        }

        public class WhenAddingPhoneticContributorWithoutName : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new PhoneticIndexAnalysisContributor();
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers.Should().ContainKey("indexify_phonetic");
            }

            [Fact]
            public void ShouldConfigureTokenFilter()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.TokenFilters.Should().ContainKey("indexify_phonetic");
            }
        }
    }
}
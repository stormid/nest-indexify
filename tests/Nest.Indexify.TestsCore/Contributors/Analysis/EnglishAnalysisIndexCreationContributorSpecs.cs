using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Xunit;

namespace Nest.Indexify.Tests.Contributors.Analysis
{
    public abstract class EnglishAnalysisIndexCreationContributorSpecs
    {
        public class WhenAddingAnalysisWithNames : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new EnglishIndexAnalysisContributor("english");
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers.Should().ContainKey("english");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers["english"].Should()
                    .BeOfType<LanguageAnalyzer>();

                var filter =
                    SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers["english"] as
                        LanguageAnalyzer;
                filter.Type.Should().Be(Language.English.ToString().ToLowerInvariant());
            }
        }

        public class WhenAddingAnalysisWithoutNames : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new EnglishIndexAnalysisContributor();
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers.Should()
                    .ContainKey("indexify_english");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers["indexify_english"].Should()
                    .BeOfType<LanguageAnalyzer>();

                var filter =
                    SharedContext.IndexCreateRequest.IndexSettings.Analysis.Analyzers["indexify_english"] as
                        LanguageAnalyzer;
                filter.Type.Should().Be(Language.English.ToString().ToLowerInvariant());
            }
        }
    }
}

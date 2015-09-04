using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.Analysis.CharFilters;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public abstract class PatternReplacementCharFilterIndexCreationContributorSpecs
    {
        public class WhenAddingFilterWithNames : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new PatternReplaceCharFilterContributor("A", "B", "pattern");
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters.Should().ContainKey("pattern");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["pattern"].Should()
                    .BeOfType<PatternReplaceCharFilter>();
            }

            [Fact]
            public void ShouldContainConfiguration()
            {
                var filter =
                    SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["pattern"] as
                        PatternReplaceCharFilter;
                filter.Should().NotBeNull();
                filter.Pattern.Should().Be("A");
                filter.Replacement.Should().Be("B");
            }
        }

        public class WhenAddingFilterWithoutName : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new PatternReplaceCharFilterContributor("A", "B");
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters.Should()
                    .ContainKey("indexify_pattern");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["indexify_pattern"].Should()
                    .BeOfType<PatternReplaceCharFilter>();
            }

            [Fact]
            public void ShouldContainConfiguration()
            {
                var filter =
                    SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["indexify_pattern"] as
                        PatternReplaceCharFilter;
                filter.Should().NotBeNull();
                filter.Pattern.Should().Be("A");
                filter.Replacement.Should().Be("B");
            }
        }
    }
}
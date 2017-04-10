using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Xunit;

namespace Nest.Indexify.Tests.Contributors.Analysis
{
    public abstract class HtmlStripCharFilterIndexCreationContributorSpecs
    {
        public class WhenAddingFilterWithNames : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new HtmlStripCharFilterContributor("html_strip");
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters.Should().ContainKey("html_strip");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["html_strip"].Should()
                    .BeOfType<HtmlStripCharFilter>();
            }
        }

        public class WhenAddingFilterWithoutName : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new HtmlStripCharFilterContributor();
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters.Should()
                    .ContainKey("indexify_htmlstrip");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["indexify_htmlstrip"].Should()
                    .BeOfType<HtmlStripCharFilter>();
            }
        }
    }

    public abstract class PathHierarchyTokenizerIndexCreationContributorSpecs
    {
        public class WhenAddingFilterWithNames : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new PathHierarchyIndexTokenizerContributor("path", "/");
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Tokenizers.Should().ContainKey("path");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Tokenizers["path"].Should()
                    .BeOfType<PathHierarchyTokenizer>();
            }

            [Fact]
            public void ShouldContainConfiguration()
            {
                var filter =
                    SharedContext.IndexCreateRequest.IndexSettings.Analysis.Tokenizers["path"] as
                        PathHierarchyTokenizer;
                filter.Should().NotBeNull();
                filter.Delimiter.Should().Be("/");
                filter.BufferSize.Should().NotHaveValue();
                filter.Replacement.Should().BeNull();
                filter.Reverse.Should().NotHaveValue();
                filter.Skip.Should().NotHaveValue();
            }
        }

        public class WhenAddingFilterWithoutName : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new PathHierarchyIndexTokenizerContributor();
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Tokenizers.Should()
                    .ContainKey("indexify_path");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.Tokenizers["indexify_path"].Should()
                    .BeOfType<PathHierarchyTokenizer>();
            }

            [Fact]
            public void ShouldContainConfiguration()
            {
                var filter =
                    SharedContext.IndexCreateRequest.IndexSettings.Analysis.Tokenizers["indexify_path"] as
                        PathHierarchyTokenizer;
                filter.Should().NotBeNull();
                filter.Delimiter.Should().BeNull();
                filter.BufferSize.Should().NotHaveValue();
                filter.Replacement.Should().BeNull();
                filter.Reverse.Should().NotHaveValue();
                filter.Skip.Should().NotHaveValue();
            }
        }
    }
}
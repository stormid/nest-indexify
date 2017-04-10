using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Xunit;

namespace Nest.Indexify.Tests.Contributors.Analysis
{
    public abstract class MappingCharFilterIndexCreationContributorSpecs
    {
        public class WhenAddingFilterWithNames : IndexCreationStrategyContext
        {
            private IEnumerable<string> _mappings;

            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new MappingCharFilterContributor(_mappings, name: "mapping");
            }

            protected override void Context()
            {
                _mappings = new[] {"a=>b"};
                base.Context();
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters.Should().ContainKey("mapping");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["mapping"].Should()
                    .BeOfType<MappingCharFilter>();
            }

            [Fact]
            public void ShouldContainConfiguration()
            {
                var filter =
                    SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["mapping"] as
                        MappingCharFilter;
                filter.Should().NotBeNull();
                filter.Mappings.Should().BeEquivalentTo(_mappings);
                filter.MappingsPath.Should().BeNull();
            }
        }

        public class WhenAddingFilterWithoutNames : IndexCreationStrategyContext
        {
            private IEnumerable<string> _mappings;

            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new MappingCharFilterContributor(_mappings);
            }

            protected override void Context()
            {
                _mappings = new[] { "a=>b" };
                base.Context();
            }

            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters.Should().ContainKey("indexify_charmapping");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["indexify_charmapping"].Should()
                    .BeOfType<MappingCharFilter>();
            }

            [Fact]
            public void ShouldContainConfiguration()
            {
                var filter =
                    SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["indexify_charmapping"] as
                        MappingCharFilter;
                filter.Should().NotBeNull();
                filter.Mappings.Should().BeEquivalentTo(_mappings);
                filter.MappingsPath.Should().BeNull();
            }
        }

        public class WhenAddingFilterWithMappingPath : IndexCreationStrategyContext
        {
            protected override IEnumerable<IElasticsearchIndexCreationContributor> Contributors()
            {
                yield return new MappingCharFilterContributor(Enumerable.Empty<string>(), @"this/is/a/path.txt");
            }
            
            [Fact]
            public void ShouldConfigureAnalyzer()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters.Should().ContainKey("indexify_charmapping");
            }

            [Fact]
            public void ShouldBeOfExpectedType()
            {
                SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["indexify_charmapping"].Should()
                    .BeOfType<MappingCharFilter>();
            }

            [Fact]
            public void ShouldContainConfiguration()
            {
                var filter =
                    SharedContext.IndexCreateRequest.IndexSettings.Analysis.CharFilters["indexify_charmapping"] as
                        MappingCharFilter;
                filter.Should().NotBeNull();
                filter.Mappings.Should().BeEmpty();
                filter.MappingsPath.Should().Be(@"this/is/a/path.txt");
            }
        }
    }
}
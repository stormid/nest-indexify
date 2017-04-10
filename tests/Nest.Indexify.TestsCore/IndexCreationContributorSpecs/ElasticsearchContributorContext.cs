using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.IndexSettings;
using Nest.Indexify.Tests.Stubs;
using Xunit;

namespace Nest.Indexify.Tests.IndexCreationContributorSpecs
{
    public abstract class ElasticsearchContributorContext : IClassFixture<ElasticClientQueryObjectTestFixture>
    {
        private readonly ElasticClientQueryObjectTestFixture _fixture;
        protected IndexSettings _indexSettings;

        protected ElasticsearchContributorContext(ElasticClientQueryObjectTestFixture fixture)
        {
            _fixture = fixture;
            Because();
        }

        protected void Because()
        {
            var strategy = new StubElasticsearchIndexCreationStrategy(_fixture.Client);
            strategy.Create(Contributors().ToArray());
            // _indexSettings = _fixture.RespondsWith<IndexSettings>();
        }

        protected abstract IEnumerable<IElasticsearchIndexContributor> Contributors();

    }

    public abstract class NumberOfShardsIndexCreationContributorSpecs : ElasticsearchContributorContext
    {
        protected NumberOfShardsIndexCreationContributorSpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        public class WithDefault : NumberOfShardsIndexCreationContributorSpecs
        {
            public WithDefault(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public void ShoulduseDefaultShardSettings()
            {
                _indexSettings.NumberOfShards.Should().NotHaveValue();
            }

            protected override IEnumerable<IElasticsearchIndexContributor> Contributors()
            {
                yield return new NumberOfShardsIndexSettingsContributor();
            }
        }

        public class WithSpecificShardCount : NumberOfShardsIndexCreationContributorSpecs
        {
            public WithSpecificShardCount(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            protected override IEnumerable<IElasticsearchIndexContributor> Contributors()
            {
                yield return new NumberOfShardsIndexSettingsContributor(2);
            }

            [Fact]
            public void ShoulduseDefaultShardSettings()
            {
                _indexSettings.NumberOfShards.Should().HaveValue().And.Be(2);
            }
        }

        public class WithInvalidShardCount : NumberOfShardsIndexCreationContributorSpecs
        {
            public WithInvalidShardCount(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            protected override IEnumerable<IElasticsearchIndexContributor> Contributors()
            {
                yield return new NumberOfShardsIndexSettingsContributor(-1);
            }

            [Fact]
            public void ShoulduseDefaultShardSettings()
            {
                _indexSettings.NumberOfShards.Should().NotHaveValue();
            }
        }
    }

    public abstract class NumberOfReplicasIndexCreationContributorSpecs : ElasticsearchContributorContext
    {
        protected NumberOfReplicasIndexCreationContributorSpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        public class WithDefault : NumberOfReplicasIndexCreationContributorSpecs
        {
            public WithDefault(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public void ShoulduseDefaultReplicaSettings()
            {
                _indexSettings.NumberOfReplicas.Should().NotHaveValue();
            }

            protected override IEnumerable<IElasticsearchIndexContributor> Contributors()
            {
                yield return new NumberOfReplicasIndexSettingsContributor();
            }
        }

        public class WithSpecificReplicaCount : NumberOfReplicasIndexCreationContributorSpecs
        {
            public WithSpecificReplicaCount(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            protected override IEnumerable<IElasticsearchIndexContributor> Contributors()
            {
                yield return new NumberOfReplicasIndexSettingsContributor(2);
            }

            [Fact]
            public void ShoulduseDefaultReplicaSettings()
            {
                _indexSettings.NumberOfReplicas.Should().HaveValue().And.Be(2);
            }
        }

        public class WithInvalidReplicaCount : NumberOfReplicasIndexCreationContributorSpecs
        {
            public WithInvalidReplicaCount(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            protected override IEnumerable<IElasticsearchIndexContributor> Contributors()
            {
                yield return new NumberOfReplicasIndexSettingsContributor(-1);
            }

            [Fact]
            public void ShoulduseDefaultReplicaSettings()
            {
                _indexSettings.NumberOfReplicas.Should().NotHaveValue();
            }
        }
    }

    public abstract class IndexSettingsCreationContributorSpecs : ElasticsearchContributorContext
    {
        protected IndexSettingsCreationContributorSpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        public class WithDefault : IndexSettingsCreationContributorSpecs
        {
            public WithDefault(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public void ShoulduseDefaultReplicaSettings()
            {
                _indexSettings.NumberOfReplicas.Should().NotHaveValue();
            }

            protected override IEnumerable<IElasticsearchIndexContributor> Contributors()
            {
                yield return new NumberOfReplicasIndexSettingsContributor();
            }
        }

        public class WithSpecificReplicaCount : IndexSettingsCreationContributorSpecs
        {
            public WithSpecificReplicaCount(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            protected override IEnumerable<IElasticsearchIndexContributor> Contributors()
            {
                yield return new NumberOfReplicasIndexSettingsContributor(2);
            }

            [Fact]
            public void ShoulduseDefaultReplicaSettings()
            {
                _indexSettings.NumberOfReplicas.Should().HaveValue().And.Be(2);
            }
        }

        public class WithInvalidReplicaCount : IndexSettingsCreationContributorSpecs
        {
            public WithInvalidReplicaCount(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            protected override IEnumerable<IElasticsearchIndexContributor> Contributors()
            {
                yield return new NumberOfReplicasIndexSettingsContributor(-1);
            }

            [Fact]
            public void ShoulduseDefaultReplicaSettings()
            {
                _indexSettings.NumberOfReplicas.Should().NotHaveValue();
            }
        }
    }

}
using System.Collections.Generic;
using FluentAssertions;
using Nest.Indexify.Contributors;
using Nest.Indexify.Contributors.IndexSettings;
using Nest.Indexify.Tests.IndexCreationContributorSpecs;
using Nest.Indexify.Tests.Stubs;
using Xunit;

namespace Nest.Indexify.Tests.Contributors
{
    public abstract class IndexSettingsContributorSpecs
    {
        public class WhenContributingShardValue : IndexCreationStrategyContext
        {
            public WhenContributingShardValue(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            [Theory]
            [InlineData(3, 3)]
            [InlineData(null, null)]
            [InlineData(-1, null)]
            public void ShouldConfigureNumberOfShards(int? shards, int? expected)
            {
                var strategy = new StubElasticsearchIndexCreationStrategy(Fixture.Client, new NumberOfShardsIndexSettingsContributor(shards));
                var result = strategy.Create();
                var indexRequest = Fixture.DeserializeRequestBody<ICreateIndexRequest>(result.IndexResponse.ApiCall);

                indexRequest.Settings.NumberOfShards.Should().Be(expected);
            }
        }

        public class WhenContributingReplicaValue : IndexCreationStrategyContext
        {
            public WhenContributingReplicaValue(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }

            [Theory]
            [InlineData(3, 3)]
            [InlineData(null, null)]
            [InlineData(-1, null)]
            public void ShouldConfigureNumberOfReplicas(int? replicas, int? expected)
            {
                var strategy = new StubElasticsearchIndexCreationStrategy(Fixture.Client, new NumberOfReplicasIndexSettingsContributor(replicas));
                var result = strategy.Create();
                var indexRequest = Fixture.DeserializeRequestBody<ICreateIndexRequest>(result.IndexResponse.ApiCall);

                indexRequest.Settings.NumberOfReplicas.Should().Be(expected);
            }
        }

        public class WhenAddDefaultIndexSettings : IndexCreationStrategyContext
        {
            [Fact]
            public void ShouldIgnoreShardsSetting()
            {
                var strategy = new StubElasticsearchIndexCreationStrategy(Fixture.Client, new IndexSettingsContributor());
                var result = strategy.Create();
                var indexRequest = Fixture.DeserializeRequestBody<ICreateIndexRequest>(result.IndexResponse.ApiCall);
                indexRequest.Settings.NumberOfShards.Should().NotHaveValue();
            }

            [Fact]
            public void ShouldIgnoreReplicasSetting()
            {
                var strategy = new StubElasticsearchIndexCreationStrategy(Fixture.Client, new IndexSettingsContributor());
                var result = strategy.Create();
                var indexRequest = Fixture.DeserializeRequestBody<ICreateIndexRequest>(result.IndexResponse.ApiCall);
                indexRequest.Settings.NumberOfReplicas.Should().NotHaveValue();
            }

            public WhenAddDefaultIndexSettings(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }
        }

        public class WhenAddDefaultIndexSettingsWithValues : IndexCreationStrategyContext
        {
            [Fact]
            public void ShouldSetShardsSetting()
            {
                var strategy = new StubElasticsearchIndexCreationStrategy(Fixture.Client, new IndexSettingsContributor(3, 2));
                var result = strategy.Create();
                var indexRequest = Fixture.DeserializeRequestBody<ICreateIndexRequest>(result.IndexResponse.ApiCall);
                indexRequest.Settings.NumberOfShards.Should().HaveValue().And.Be(3);
            }

            [Fact]
            public void ShouldSetReplicasSetting()
            {
                var strategy = new StubElasticsearchIndexCreationStrategy(Fixture.Client, new IndexSettingsContributor(3, 2));
                var result = strategy.Create();
                var indexRequest = Fixture.DeserializeRequestBody<ICreateIndexRequest>(result.IndexResponse.ApiCall);
                indexRequest.Settings.NumberOfReplicas.Should().HaveValue().And.Be(2);
            }

            public WhenAddDefaultIndexSettingsWithValues(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
            {
            }
        }
    }
}
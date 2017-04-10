using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using Nest.Indexify.Contributors;
using Nest.Indexify.Tests.Specification;
using Nest.Indexify.Tests.Stubs;
using Xunit;

namespace Nest.Indexify.Tests
{
    //public class ElasticsearchIndexCreationStrategySpecs : ContextSpecification<ElasticClientContext>
    //{
    //    private IElasticsearchIndexCreationStrategy _strategy;

    //    private IEnumerable<IElasticsearchIndexCreationContributor> _stubContributors;
    //    private IElasticsearchIndexCreationStrategyResult _result;

    //    protected override void Context()
    //    {
    //        _stubContributors = new[]
    //        {
    //            new StubElasticsearchIndexCreationContributor(true, 3),
    //            new StubElasticsearchIndexCreationContributor(true, 2),
    //            new StubElasticsearchIndexCreationContributor(true, 1)
    //        };
    //        _strategy = new StubElasticsearchIndexCreationStrategy(SharedContext.Client.Object, _stubContributors.ToArray());
    //    }

    //    protected override void Because()
    //    {
    //        _result = _strategy.Create();
    //    }

    //    [Fact]
    //    public void ShouldCallElasticClientCreateIndex()
    //    {
    //        SharedContext.Client.Verify(v => v.CreateIndex(It.IsAny<Func<CreateIndexDescriptor, CreateIndexDescriptor>>()), Times.Once);
    //    }


    //    [Fact]
    //    public void ShouldReturnResult()
    //    {
    //        _result.Success.Should().BeTrue();
    //        _result.Contributors.Count().Should().Be(3);
    //        _result.Contributors.All(x => x.HasContributed).Should().BeTrue();
    //    }

    //    [Fact]
    //    public void ShouldHaveCalledAllContributors()
    //    {
    //        _stubContributors.OfType<StubElasticsearchIndexCreationContributor>()
    //            .All(x => x.HasContributed)
    //            .Should()
    //            .BeTrue();
    //    }
    //}

    //public class ElasticsearchIndexCreationStrategyAsyncSpecs : ContextSpecification<ElasticClientContext>
    //{
    //    private IElasticsearchIndexCreationStrategy _strategy;

    //    private IEnumerable<IElasticsearchIndexCreationContributor> _stubContributors;
    //    private IElasticsearchIndexCreationStrategyResult _result;

    //    protected override void Context()
    //    {
    //        _stubContributors = new[]
    //        {
    //            new StubElasticsearchIndexCreationContributor(true, 3),
    //            new StubElasticsearchIndexCreationContributor(true, 2),
    //            new StubElasticsearchIndexCreationContributor(true, 1)
    //        };
    //        _strategy = new StubElasticsearchIndexCreationStrategy(SharedContext.Client.Object, _stubContributors.ToArray());
    //    }
        
    //    protected async override void Because()
    //    {
    //        _result = await _strategy.CreateAsync();
    //    }

    //    [Fact]
    //    public void ShouldCallElasticClientCreateIndex()
    //    {
    //        SharedContext.Client.Verify(v => v.CreateIndexAsync(It.IsAny<Func<CreateIndexDescriptor, CreateIndexDescriptor>>()), Times.Once);
    //    }

    //    [Fact]
    //    public void ShouldReturnResult()
    //    {
    //        _result.Success.Should().BeTrue();
    //        _result.Contributors.Count().Should().Be(3);
    //    }

    //    [Fact]
    //    public void ShouldHaveCalledAllContributors()
    //    {
    //        _stubContributors.OfType<StubElasticsearchIndexCreationContributor>()
    //            .All(x => x.HasContributed)
    //            .Should()
    //            .BeTrue();
    //    }
    //}
}
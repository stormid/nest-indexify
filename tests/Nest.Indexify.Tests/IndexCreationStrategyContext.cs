using System.Collections.Generic;
using System.Linq;
using Nest.Indexify.Contributors;
using Nest.Indexify.Tests.Specification;
using Nest.Indexify.Tests.Stubs;

namespace Nest.Indexify.Tests
{
    public abstract class IndexCreationStrategyContext : ContextSpecification<ElasticClientContext>
    {
        private IElasticsearchIndexCreationStrategy _strategy;
        protected IElasticsearchIndexCreationStrategyResult _result;

        protected override void Context()
        {
            _strategy = new StubElasticsearchIndexCreationStrategy(SharedContext.Client.Object, Contributors().ToArray());
        }

        protected abstract IEnumerable<IElasticsearchIndexCreationContributor> Contributors();

        protected IndexCreationStrategyContext() : base(new ElasticClientContext())
        {
        }

        protected override void Because()
        {
            _result = _strategy.Create();
        }
    }
}
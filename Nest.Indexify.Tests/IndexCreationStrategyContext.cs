using System.Collections.Generic;
using System.Linq;
using Nest.Indexify.Contributors;
using Nest.Indexify.Tests.Specification;

namespace Nest.Indexify.Tests
{
    public abstract class IndexCreationStrategyContext : ContextSpecification<ElasticClientContext>
    {
        private IElasticsearchIndexCreationStrategy _strategy;

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
            _strategy.Create();
        }
    }
}
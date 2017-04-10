using System.Collections.Generic;
using System.Linq;
using Nest.Indexify.Contributors;

namespace Nest.Indexify.Tests.Stubs
{
    public class StubElasticsearchIndexCreationStrategy : ElasticsearchIndexCreationStrategy
    {
        public IEnumerable<IElasticsearchIndexCreationContributor> Contributors { get; private set; }
        public StubElasticsearchIndexCreationStrategy(IElasticClient client, params IElasticsearchIndexContributor[] contributors) : base(client)
        {
            foreach (var c in contributors)
            {
                AddContributor(c);
            }
        }

        protected override CreateIndexDescriptor ContributeCore(CreateIndexDescriptor descriptor, IEnumerable<IElasticsearchIndexCreationContributor> contributors)
        {
            var c = contributors.ToList();
            Contributors = c;
            return base.ContributeCore(descriptor, c);
        }
    }
}
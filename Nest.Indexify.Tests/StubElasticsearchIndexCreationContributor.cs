using System.Diagnostics;
using Nest.Indexify.Contributors;

namespace Nest.Indexify.Tests
{
    public class StubElasticsearchIndexCreationContributor : ElasticsearchIndexCreationContributor
    {
        public bool HasContributed { get; private set; }

        public StubElasticsearchIndexCreationContributor(int order) : base(order) { }

        public override void ContributeCore(CreateIndexDescriptor descriptor)
        {
            Debug.WriteLine("Contributed {0}", GetType().Name);
            HasContributed = true;
        }
    }
}
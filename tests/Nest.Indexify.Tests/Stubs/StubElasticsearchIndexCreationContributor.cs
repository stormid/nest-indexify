using System.Diagnostics;
using Nest.Indexify.Contributors;

namespace Nest.Indexify.Tests.Stubs
{
    public class StubElasticsearchIndexCreationContributor : ElasticsearchIndexCreationContributor
    {
        private readonly bool _shouldContribute;
        public bool HasContributed { get; private set; }

        public StubElasticsearchIndexCreationContributor(bool shouldContribute = true, int order = 0) : base(order)
        {
            _shouldContribute = shouldContribute;
        }

        public override bool CanContribute(ICreateIndexRequest indexRequest)
        {
            return _shouldContribute;
        }

        public override void ContributeCore(CreateIndexDescriptor descriptor)
        {
            Debug.WriteLine("Contributed {0}", GetType().Name);
            HasContributed = true;
        }
    }
}
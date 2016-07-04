using System.Diagnostics;
using Nest.Indexify.Contributors;

namespace Nest.Indexify.Tests.Stubs
{
    public class StubElasticsearchIndexCreationContributor : ElasticsearchIndexCreationContributor
    {
        private readonly bool _shouldContribute;

        public StubElasticsearchIndexCreationContributor(bool shouldContribute = true, int order = 0) : base(order)
        {
            _shouldContribute = shouldContribute;
        }

        protected override bool CanContributeCore(ICreateIndexRequest indexRequest, IElasticClient client)
        {
            return _shouldContribute;
        }

        public override void ContributeCore(CreateIndexDescriptor descriptor, IElasticClient client)
        {
            Debug.WriteLine("Contributed {0}", GetType().Name);
        }
    }
}
using System;
using System.Diagnostics;

namespace Nest.Indexify.Contributors
{
    public abstract class ElasticsearchIndexCreationContributor : ElasticsearchIndexContributor, IElasticsearchIndexCreationContributor
    {
        protected ElasticsearchIndexCreationContributor(int order = 0) : base(order)
        {
        }

        public bool CanContribute(ICreateIndexRequest indexRequest, IElasticClient client)
        {
            return CanContributeCore(indexRequest, client);
        }

        protected virtual bool CanContributeCore(ICreateIndexRequest indexRequest, IElasticClient client)
        {
            return true;
        }

        public void Contribute(CreateIndexDescriptor descriptor, IElasticClient client)
        {
            if (CanContribute(descriptor, client))
            {
                ContributeCore(descriptor, client);
            }
            else
            {
                ContributionRejected();
            }
        }

        public virtual void ContributionRejected()
        {
            Trace.TraceWarning("Failed to contribute {0} to index creation", this);
        }

        public abstract void ContributeCore(CreateIndexDescriptor descriptor, IElasticClient client);

        public override string ToString()
        {
            return $"{GetType().Name}({Order})";
        }
    }
}
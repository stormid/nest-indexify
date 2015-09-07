using System.Diagnostics;

namespace Nest.Indexify.Contributors
{
    public abstract class ElasticsearchIndexCreationContributor : IElasticsearchIndexCreationContributor
    {
        protected ElasticsearchIndexCreationContributor(int order = 0)
        {
            Order = order;
        }

        public int CompareTo(IElasticsearchIndexCreationContributor other)
        {
            if (Order == other.Order) return 1; // ensures that duplicates with the same order value are still added to the set

            return Order.CompareTo(other.Order);
        }

        public int Order { get; private set; }

        public virtual bool CanContribute(ICreateIndexRequest indexRequest)
        {
            return true;
        }

        public void Contribute(CreateIndexDescriptor descriptor)
        {
            if (CanContribute(descriptor))
            {
                ContributeCore(descriptor);
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

        public abstract void ContributeCore(CreateIndexDescriptor descriptor);

        public override string ToString()
        {
            return string.Format("{0}({1})", GetType().Name, Order);
        }
    }
}
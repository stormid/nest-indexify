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
            return Order.CompareTo(other.Order);
        }

        public int Order { get; private set; }

        public abstract void Contribute(CreateIndexDescriptor descriptor);
    }
}
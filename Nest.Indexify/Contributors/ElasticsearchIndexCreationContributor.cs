namespace Nest.Indexify.Contributors
{
    public abstract class ElasticsearchIndexCreationContributor : IElasticsearchIndexCreationContributor
    {
        public int CompareTo(IElasticsearchIndexCreationContributor other)
        {
            return Order.CompareTo(other.Order);
        }

        public int Order { get; protected set; }

        public abstract void Contribute(CreateIndexDescriptor descriptor);
    }
}
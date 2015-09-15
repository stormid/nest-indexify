namespace Nest.Indexify.Contributors
{
    public abstract class ElasticsearchIndexContributor : IElasticsearchIndexContributor
    {
        protected ElasticsearchIndexContributor(int order = 0)
        {
            Order = order;
        }

        public int CompareTo(IElasticsearchIndexContributor other)
        {
            if (Order == other.Order) return 1; // ensures that duplicates with the same order value are still added to the set

            return Order.CompareTo(other.Order);
        }

        public int Order { get; }
    }
}
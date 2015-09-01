using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest.Indexify.Contributors;

namespace Nest.Indexify
{
    public abstract class ElasticsearchIndexCreationStrategy : IElasticsearchIndexCreationStrategy
    {
        private readonly IElasticClient _client;
        private readonly ISet<IElasticsearchIndexCreationContributor> _contributors = new SortedSet<IElasticsearchIndexCreationContributor>();

        protected ElasticsearchIndexCreationStrategy(IElasticClient client)
        {
            _client = client;
        }

        public void Create(params IElasticsearchIndexCreationContributor[] additionalContributors)
        {
            var indexName = _client.Infer.DefaultIndex;
            _contributors.UnionWith(additionalContributors);

            _client.CreateIndex(indexName, i => ContributeCore(i, _contributors));
        }

        public Task CreateAsync(params IElasticsearchIndexCreationContributor[] additionalContributors)
        {
            var indexName = _client.Infer.DefaultIndex;
            _contributors.UnionWith(additionalContributors);

            return _client.CreateIndexAsync(indexName, i => ContributeCore(i, _contributors));
        }

        protected void AddContributor(IElasticsearchIndexCreationContributor contributor)
        {
            _contributors.Add(contributor);
        }

        protected virtual CreateIndexDescriptor ContributeCore(CreateIndexDescriptor descriptor, IEnumerable<IElasticsearchIndexCreationContributor> contributors)
        {
            return Contribute(descriptor, contributors.ToList());
        }

        protected virtual CreateIndexDescriptor Contribute(CreateIndexDescriptor descriptor, IList<IElasticsearchIndexCreationContributor> contributors)
        {
            foreach (var contributor in contributors)
            {
                contributor.Contribute(descriptor);
            }

            return descriptor;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest.Indexify.Contributors;

namespace Nest.Indexify
{
    public abstract class ElasticsearchIndexCreationStrategy : IElasticsearchIndexCreationStrategy
    {
        private readonly IElasticClient _client;
        private readonly ISet<IElasticsearchIndexContributor> _contributors = new SortedSet<IElasticsearchIndexContributor>();

        protected ElasticsearchIndexCreationStrategy(IElasticClient client)
        {
            _client = client;
        }

        public void Create(params IElasticsearchIndexContributor[] additionalContributors)
        {
            var indexName = _client.Infer.DefaultIndex;
            _contributors.UnionWith(additionalContributors);

            indexName = PreIndexContribution(_contributors.OfType<IElasticsearchIndexPreCreationContributor>(), indexName);

            try
            {
                var indexResponse = _client.CreateIndex(i =>
                {
                    i.Index(indexName);
                    ContributeCore(i, _contributors.OfType<IElasticsearchIndexCreationContributor>());
                    return i;
                });

                OnCompleted(_contributors.OfType<IElasticsearchIndexCreationSuccessContributor>(), indexResponse);
            }
            catch (Exception ex)
            {
                OnError(_contributors.OfType<IElasticsearchIndexCreationFailureContributor>(), ex);
                throw;
            }
        }

        private string PreIndexContribution(IEnumerable<IElasticsearchIndexPreCreationContributor> contributors, string indexName)
        {
            foreach (var contrib in contributors)
            {
                indexName = contrib.OnPreCreate(_client, indexName);
            }
            return indexName;
        }

        private void OnError(IEnumerable<IElasticsearchIndexContributor> contributors, Exception ex)
        {
            foreach (var c in contributors.OfType<IElasticsearchIndexCreationFailureContributor>())
            {
                c.OnFailure(_client, ex);
            }
        }

        private void OnCompleted(IEnumerable<IElasticsearchIndexCreationSuccessContributor> contributors, IIndicesOperationResponse indexResponse)
        {
            foreach (var c in contributors)
            {
                c.OnSuccess(_client, indexResponse);
            }
        }

        public async Task CreateAsync(params IElasticsearchIndexContributor[] additionalContributors)
        {
            var indexName = _client.Infer.DefaultIndex;
            _contributors.UnionWith(additionalContributors);

            indexName = PreIndexContribution(_contributors.OfType<IElasticsearchIndexPreCreationContributor>(), indexName);

            try
            {
                var indexResponse = await _client.CreateIndexAsync(i =>
                {
                    i.Index(indexName);
                    ContributeCore(i, _contributors.OfType<IElasticsearchIndexCreationContributor>());
                    return i;
                });

                OnCompleted(_contributors.OfType<IElasticsearchIndexCreationSuccessContributor>(), indexResponse);
            }
            catch (Exception ex)
            {
                OnError(_contributors.OfType<IElasticsearchIndexCreationFailureContributor>(), ex);
                throw;
            }
        }

        protected void AddContributor(IElasticsearchIndexContributor contributor)
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
                contributor.Contribute(descriptor, _client);
            }

            return descriptor;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Elasticsearch.Net;
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

        private IElasticsearchIndexCreationStrategyResult CreateResultCore(IList<IElasticsearchIndexContributor> contributors, IIndicesOperationResponse response, Exception ex)
        {
            return CreateResult(contributors, (ex == null && response.IsValid), response, ex);
        }

        protected virtual IElasticsearchIndexCreationStrategyResult CreateResult(IList<IElasticsearchIndexContributor> contributors, bool success, IIndicesOperationResponse response, Exception ex)
        {
            return new ElasticsearchIndexCreationStrategyResult(contributors, success, response, ex);
        }

        public IElasticsearchIndexCreationStrategyResult Create(params IElasticsearchIndexContributor[] additionalContributors)
        {
            var contribCollection = new SortedSet<IElasticsearchIndexContributor>(_contributors);
            var indexName = _client.Infer.DefaultIndex;
            contribCollection.UnionWith(additionalContributors);

            indexName = PreIndexContribution(contribCollection.OfType<IElasticsearchIndexPreCreationContributor>().ToList(), indexName);

            try
            {
                var indexResponse = _client.CreateIndex(i =>
                {
                    i.Index(indexName);
                    ContributeCore(i, contribCollection.OfType<IElasticsearchIndexCreationContributor>().ToList());
                    return i;
                });

                if (indexResponse.IsValid)
                {
                    OnCompleted(contribCollection.OfType<IElasticsearchIndexCreationSuccessContributor>().ToList(), indexResponse);
                }
                else
                {
                    OnError(contribCollection.OfType<IElasticsearchIndexCreationFailureContributor>().ToList(), indexResponse);
                }
                return CreateResultCore(contribCollection.ToList(), indexResponse, null);
            }
            catch (Exception ex)
            {
                OnError(contribCollection.OfType<IElasticsearchIndexCreationFailureContributor>().ToList(), ex: ex);
                return CreateResultCore(contribCollection.ToList(), null, ex);
            }
        }

        private string PreIndexContribution(IList<IElasticsearchIndexPreCreationContributor> contributors, string indexName)
        {
            foreach (var contrib in contributors)
            {
                indexName = contrib.OnPreCreate(_client, indexName);
            }
            return indexName;
        }

        private void OnError(IList<IElasticsearchIndexCreationFailureContributor> contributors, IIndicesOperationResponse response = null, Exception ex = null)
        {
            foreach (var c in contributors)
            {
                c.OnFailure(_client, response, ex);
            }
        }

        private void OnCompleted(IList<IElasticsearchIndexCreationSuccessContributor> contributors, IIndicesOperationResponse indexResponse)
        {
            foreach (var c in contributors)
            {
                c.OnSuccess(_client, indexResponse);
            }
        }
        
        public async Task<IElasticsearchIndexCreationStrategyResult> CreateAsync(params IElasticsearchIndexContributor[] additionalContributors)
        {
            var contribCollection = new SortedSet<IElasticsearchIndexContributor>(_contributors);
            var indexName = _client.Infer.DefaultIndex;
            contribCollection.UnionWith(additionalContributors);

            indexName = PreIndexContribution(contribCollection.OfType<IElasticsearchIndexPreCreationContributor>().ToList(), indexName);

            try
            {
                var indexResponse = await _client.CreateIndexAsync(i =>
                {
                    i.Index(indexName);
                    ContributeCore(i, contribCollection.OfType<IElasticsearchIndexCreationContributor>().ToList());
                    return i;
                });

                if (indexResponse.IsValid)
                {
                    OnCompleted(contribCollection.OfType<IElasticsearchIndexCreationSuccessContributor>().ToList(), indexResponse);
                }
                else
                {
                    OnError(contribCollection.OfType<IElasticsearchIndexCreationFailureContributor>().ToList(), indexResponse);
                }
                return CreateResultCore(contribCollection.ToList(), indexResponse, null);
            }
            catch (Exception ex)
            {
                OnError(contribCollection.OfType<IElasticsearchIndexCreationFailureContributor>().ToList(), ex: ex);
                return CreateResultCore(contribCollection.ToList(), null, ex);
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
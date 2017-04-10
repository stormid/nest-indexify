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

        private IElasticsearchIndexCreationStrategyResult CreateResultCore(IList<IElasticsearchIndexContributor> contributors, ICreateIndexResponse response, Exception ex)
        {
            return CreateResult(contributors, (ex == null && response.IsValid), response, ex);
        }

        private string GetDefaultIndex()
        {
            return _client.ConnectionSettings.DefaultIndex;
        }

        protected virtual IElasticsearchIndexCreationStrategyResult CreateResult(IList<IElasticsearchIndexContributor> contributors, bool success, ICreateIndexResponse response, Exception ex)
        {
            return new ElasticsearchIndexCreationStrategyResult(contributors, success, response, ex);
        }

        public IElasticsearchIndexCreationStrategyResult Create(params IElasticsearchIndexContributor[] additionalContributors)
        {
            var contribCollection = new SortedSet<IElasticsearchIndexContributor>(_contributors);
            var indexName = GetDefaultIndex();
            contribCollection.UnionWith(additionalContributors);

            indexName = PreIndexContribution(contribCollection.OfType<IElasticsearchIndexPreCreationContributor>().ToList(), indexName);

            try
            {
                var indexResponse = _client.CreateIndex(indexName,  i =>
                {
                    ContributeIndexSettings(i, contribCollection);
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

        private CreateIndexDescriptor ContributeIndexSettings(CreateIndexDescriptor i, SortedSet<IElasticsearchIndexContributor> contribCollection)
        {
            // TODO : re-evaluate all of this ugly mess
            i = i.Settings(settings =>
            {
                foreach (var setting in contribCollection.OfType<IElasticsearchIndexSettingsContributor>().ToList())
                {
                    setting.Contribute(settings, _client);
                }
                foreach (var setting in contribCollection.OfType<IElasticsearchIndexAnalysisContributor>().ToList())
                {
                    settings.Analysis(a =>
                    {
                        setting.Contribute(a, _client);
                        return a;
                    });
                }
                return settings;
            });
            return i;
        }

        private string PreIndexContribution(IList<IElasticsearchIndexPreCreationContributor> contributors, string indexName)
        {
            foreach (var contrib in contributors)
            {
                indexName = contrib.OnPreCreate(_client, indexName);
            }
            return indexName;
        }

        private void OnError(IList<IElasticsearchIndexCreationFailureContributor> contributors, ICreateIndexResponse response = null, Exception ex = null)
        {
            foreach (var c in contributors)
            {
                c.OnFailure(_client, response, ex);
            }
        }

        private void OnCompleted(IList<IElasticsearchIndexCreationSuccessContributor> contributors, ICreateIndexResponse indexResponse)
        {
            foreach (var c in contributors)
            {
                c.OnSuccess(_client, indexResponse);
            }
        }
        
        public async Task<IElasticsearchIndexCreationStrategyResult> CreateAsync(params IElasticsearchIndexContributor[] additionalContributors)
        {
            var contribCollection = new SortedSet<IElasticsearchIndexContributor>(_contributors);
            var indexName = GetDefaultIndex();
            contribCollection.UnionWith(additionalContributors);

            indexName = PreIndexContribution(contribCollection.OfType<IElasticsearchIndexPreCreationContributor>().ToList(), indexName);

            try
            {
                var indexResponse = await _client.CreateIndexAsync(indexName, i =>
                {
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
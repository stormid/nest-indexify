using System.Collections.Generic;

namespace Nest.Indexify.Contributors
{
    public abstract class CompositeElasticsearchIndexSettingsContributor : ElasticsearchIndexSettingsContributor
    {
        private readonly ISet<IElasticsearchIndexSettingsContributor> _contributors = new SortedSet<IElasticsearchIndexSettingsContributor>();

        protected void Add(IElasticsearchIndexSettingsContributor contributor)
        {
            _contributors.Add(contributor);
        }

        protected CompositeElasticsearchIndexSettingsContributor(int order = 0) : base(order)
        {
        }

        protected sealed override bool CanContributeCore(IIndexSettings indexRequest, IElasticClient client)
        {
            return true;
        }

        public override void ContributeCore(IndexSettingsDescriptor descriptor, IElasticClient client)
        {
            foreach (var contributor in _contributors)
            {
                contributor.Contribute(descriptor, client);
            }
        }
    }


    public abstract class CompositeElasticsearchIndexCreationContributor : ElasticsearchIndexCreationContributor
	{
	    private readonly ISet<IElasticsearchIndexCreationContributor> _contributors = new SortedSet<IElasticsearchIndexCreationContributor>();

	    protected void Add(IElasticsearchIndexCreationContributor contributor)
	    {
	        _contributors.Add(contributor);
	    }

		protected CompositeElasticsearchIndexCreationContributor(int order = 0) : base(order)
		{
		}
        
        protected sealed override bool CanContributeCore(ICreateIndexRequest indexRequest, IElasticClient client)
        {
	        return true;
	    }

	    public override void ContributeCore(CreateIndexDescriptor descriptor, IElasticClient client)
		{
			foreach (var contributor in _contributors)
			{
				contributor.Contribute(descriptor, client);
			}
		}
	}
}
using System.Collections.Generic;

namespace Nest.Indexify.Contributors
{
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

		public override void Contribute(CreateIndexDescriptor descriptor)
		{
			foreach (var contributor in _contributors)
			{
				contributor.Contribute(descriptor);
			}
		}
	}
}
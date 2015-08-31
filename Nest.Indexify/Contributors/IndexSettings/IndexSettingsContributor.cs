namespace Nest.Indexify.Contributors.IndexSettings
{
	public class IndexSettingsContributor : ElasticsearchIndexCreationContributor
	{
	    private readonly int? _shards;
	    private readonly int? _replicas;

	    public IndexSettingsContributor(int? shards, int? replicas, int order = 99)
	    {
	        _shards = shards;
	        _replicas = replicas;
	        Order = order;
	    }

		public override void Contribute(CreateIndexDescriptor descriptor)
		{
			if (_shards.HasValue && _shards.Value > 0)
			{
				descriptor.NumberOfShards(_shards.Value);
			}

			if (_replicas.HasValue && _replicas >= 0)
			{
				descriptor.NumberOfReplicas(_replicas.Value);
			}	
		}
	}
}

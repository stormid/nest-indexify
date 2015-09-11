namespace Nest.Indexify.Contributors.IndexSettings
{
    public class NumberOfShardsIndexSettingsContributor : ElasticsearchIndexCreationContributor
    {
        private readonly int? _shards;

        public NumberOfShardsIndexSettingsContributor(int? shards = null)
        {
            _shards = shards;
        }

        public override void ContributeCore(CreateIndexDescriptor descriptor, IElasticClient client)
        {
            if (_shards.HasValue && _shards.Value > 0)
            {
                descriptor.NumberOfShards(_shards.Value);
            }
        }
    }
}
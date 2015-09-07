namespace Nest.Indexify.Contributors.IndexSettings
{
    public class NumberOfReplicasIndexSettingsContributor : ElasticsearchIndexCreationContributor
    {
        private readonly int? _replicas;

        public NumberOfReplicasIndexSettingsContributor(int? replicas = null, int order = 99) : base(order)
        {
            _replicas = replicas;
        }

        public override bool CanContribute(ICreateIndexRequest indexRequest)
        {
            return true;
        }

        public override void ContributeCore(CreateIndexDescriptor descriptor)
        {
            if (_replicas.HasValue && _replicas >= 0)
            {
                descriptor.NumberOfReplicas(_replicas.Value);
            }	
        }
    }
}
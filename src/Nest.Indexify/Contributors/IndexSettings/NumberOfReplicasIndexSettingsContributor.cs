using System;

namespace Nest.Indexify.Contributors.IndexSettings
{
    public class NumberOfReplicasIndexSettingsContributor : ElasticsearchIndexSettingsContributor
    {
        private readonly int? _replicas;

        public NumberOfReplicasIndexSettingsContributor(int? replicas = null, int order = 99) : base(order)
        {
            _replicas = replicas;
        }

        protected override bool CanContributeCore(IIndexSettings indexSettings, IElasticClient client)
        {
            return true;
        }

        public override void ContributeCore(IndexSettingsDescriptor descriptor, IElasticClient client)
        {
            if (_replicas.HasValue && _replicas >= 0)
            {
                descriptor.NumberOfReplicas(_replicas.Value);
            }
        }
    }
}
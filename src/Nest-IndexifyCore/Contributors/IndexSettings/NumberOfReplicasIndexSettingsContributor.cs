using System;

namespace Nest.Indexify.Contributors.IndexSettings
{
    public class NumberOfReplicasIndexSettingsContributor : ElasticsearchIndexCreationContributor
    {
        private readonly int? _replicas;

        public NumberOfReplicasIndexSettingsContributor(int? replicas = null, int order = 99) : base(order)
        {
            _replicas = replicas;
        }

        protected override bool CanContributeCore(ICreateIndexRequest indexRequest, IElasticClient client)
        {
            return true;
        }

        public override void ContributeCore(CreateIndexDescriptor descriptor, IElasticClient client)
        {
            if (_replicas.HasValue && _replicas >= 0)
            {
                descriptor.Settings(s => s.NumberOfReplicas(_replicas.Value));
            }	
        }
    }
}
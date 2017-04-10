namespace Nest.Indexify.Contributors.IndexSettings
{
    public class IndexSettingsContributor : CompositeElasticsearchIndexCreationContributor
    {
        public IndexSettingsContributor(int? numberOfShards = null, int? numberOfReplicas = null)
        {
            Add(new NumberOfShardsIndexSettingsContributor(numberOfShards));
            Add(new NumberOfReplicasIndexSettingsContributor(numberOfReplicas));
        }
    }
}

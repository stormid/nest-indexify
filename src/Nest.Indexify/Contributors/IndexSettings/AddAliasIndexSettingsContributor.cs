namespace Nest.Indexify.Contributors.IndexSettings
{
    public class AddAliasIndexSettingsContributor : ElasticsearchIndexCreationContributor
    {
        private readonly string _aliasName;

        public AddAliasIndexSettingsContributor(string aliasName)
        {
            _aliasName = aliasName;
        }

        public override void ContributeCore(CreateIndexDescriptor descriptor, IElasticClient client)
        {
            descriptor.Aliases(a => a.Alias(_aliasName, AddAliasCore));
        }

        protected override bool CanContributeCore(ICreateIndexRequest indexRequest, IElasticClient client)
        {
            return indexRequest.Aliases == null || !indexRequest.Aliases.ContainsKey(_aliasName);
        }

        private AliasDescriptor AddAliasCore(AliasDescriptor descriptor)
        {
            AddAlias(descriptor);
            return descriptor;
        }

        protected virtual void AddAlias(AliasDescriptor descriptor)
        {
        }
    }
}
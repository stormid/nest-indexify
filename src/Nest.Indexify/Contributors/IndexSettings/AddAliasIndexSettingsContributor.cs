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
            descriptor.AddAlias(_aliasName, AddAliasCore);
        }

        protected override bool CanContributeCore(ICreateIndexRequest indexRequest, IElasticClient client)
        {
            return indexRequest.IndexSettings.Aliases == null || !indexRequest.IndexSettings.Aliases.ContainsKey(_aliasName);
        }

        private CreateAliasDescriptor AddAliasCore(CreateAliasDescriptor descriptor)
        {
            AddAlias(descriptor);
            return descriptor;
        }

        protected virtual void AddAlias(CreateAliasDescriptor descriptor)
        {
        }
    }
}
namespace Nest.Indexify.Contributors.IndexSettings
{
    public class AddAliasIndexSettingsContributor : ElasticsearchIndexCreationContributor
    {
        private readonly string _aliasName;

        public AddAliasIndexSettingsContributor(string aliasName)
        {
            _aliasName = aliasName;
        }

        public override void ContributeCore(CreateIndexDescriptor descriptor)
        {
            descriptor.AddAlias(_aliasName, AddAliasCore);
        }

        public override bool CanContribute(ICreateIndexRequest indexRequest)
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
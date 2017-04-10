namespace Nest.Indexify.Contributors
{
    public abstract class ElasticsearchIndexSettingsContributor : ElasticsearchIndexContributor, IElasticsearchIndexSettingsContributor
    {
        protected ElasticsearchIndexSettingsContributor(int order = 0) : base(order)
        {
        }

        public bool CanContribute(IIndexSettings indexSettings, IElasticClient client)
        {
            return CanContributeCore(indexSettings, client);
        }

        protected virtual bool CanContributeCore(IIndexSettings indexSettings, IElasticClient client)
        {
            return true;
        }

        public void Contribute(IndexSettingsDescriptor descriptor, IElasticClient client)
        {
            if (CanContribute((descriptor as IPromise<IIndexSettings>).Value, client))
            {
                ContributeCore(descriptor, client);
                ContributionComplete();
            }
            else
            {
                ContributionRejected();
            }
        }

        public virtual void ContributionRejected()
        {
        }

        public abstract void ContributeCore(IndexSettingsDescriptor descriptor, IElasticClient client);

        public override string ToString()
        {
            return $"{GetType().Name}({Order})";
        }
    }

    public abstract class ElasticsearchIndexCreationContributor : ElasticsearchIndexContributor, IElasticsearchIndexCreationContributor
    {
        protected ElasticsearchIndexCreationContributor(int order = 0) : base(order)
        {
        }

        public bool CanContribute(ICreateIndexRequest indexRequest, IElasticClient client)
        {
            return CanContributeCore(indexRequest, client);
        }

        protected virtual bool CanContributeCore(ICreateIndexRequest indexRequest, IElasticClient client)
        {
            return true;
        }

        public void Contribute(CreateIndexDescriptor descriptor, IElasticClient client)
        {
            if (CanContribute(descriptor, client))
            {
                ContributeCore(descriptor, client);
                ContributionComplete();
            }
            else
            {
                ContributionRejected();
            }
        }

        public virtual void ContributionRejected()
        {
        }

        public abstract void ContributeCore(CreateIndexDescriptor descriptor, IElasticClient client);

        public override string ToString()
        {
            return $"{GetType().Name}({Order})";
        }
    }
}
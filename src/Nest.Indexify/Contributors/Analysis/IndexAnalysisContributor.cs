using System.Collections.Generic;

namespace Nest.Indexify.Contributors.Analysis
{
    public abstract class ElasticsearchIndexContributor<TDescriptor, TInterface> : ElasticsearchIndexContributor
        where TDescriptor : DescriptorBase<TDescriptor, TInterface>, TInterface
        where TInterface : class
    {
        protected ElasticsearchIndexContributor(int order = 0) : base(order)
        {
        }

        public void Contribute(TDescriptor descriptor, IElasticClient client)
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

        public bool CanContribute(TInterface instance, IElasticClient client)
        {
            return CanContributeCore(instance, client);
        }

        protected virtual bool CanContributeCore(TInterface instance, IElasticClient client)
        {
            return true;
        }

        public virtual void ContributionRejected()
        {
        }

        public abstract void ContributeCore(TDescriptor descriptor, IElasticClient client);

        public override string ToString()
        {
            return $"{GetType().Name}({Order})";
        }
    }

    public abstract class ElasticsearchIndexAnalysisContributor : ElasticsearchIndexContributor<AnalysisDescriptor, IAnalysis>, IElasticsearchIndexAnalysisContributor
    {
        protected ElasticsearchIndexAnalysisContributor(int order = 0) : base(order)
        {
        }
    }

    //public abstract class IndexAnalysisContributor<T> : ElasticsearchIndexCreationContributor where T : TAnalyzer where TAnalyzer : class
    //{
    //	protected abstract IEnumerable<KeyValuePair<string, T>> Build();

    //	protected IndexAnalysisContributor(int order = 0) : base(order) { }

    //	protected virtual bool CanContribute(KeyValuePair<string, T> setting, FluentDictionary<string, T> existing)
    //	{
    //		return !existing.ContainsKey(setting.Key);
    //	}

    //	public sealed override void ContributeCore(CreateIndexDescriptor descriptor, IElasticClient client)
    //	{
    //	    descriptor
    //	        .Settings(s => s
    //	            .Analysis(ContributeCore)
    //	        );
    //	}

    //	protected virtual AnalysisDescriptor ContributeCore(AnalysisDescriptor descriptor)
    //	{
    //		return Contribute(descriptor, Build());
    //	}

    //	protected abstract AnalysisDescriptor Contribute(AnalysisDescriptor descriptor, IEnumerable<KeyValuePair<string, T>> build);
    //}
}
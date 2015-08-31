using System.Collections.Generic;
using Nest.Indexify.Contributors;

namespace Nest.Indexify.Analysis
{
	public abstract class IndexAnalysisContributor<T> : IElasticsearchIndexCreationContributor where T : IAnalysisSetting
	{
		protected abstract IEnumerable<KeyValuePair<string, T>> Build();

		protected IndexAnalysisContributor(int order = 0)
		{
			Order = order;
		}

		protected virtual bool CanContribute(KeyValuePair<string, T> setting, FluentDictionary<string, T> existing)
		{
			return !existing.ContainsKey(setting.Key);
		}

		public int Order { get; protected set; }

		public void Contribute(CreateIndexDescriptor descriptor)
		{
			descriptor.Analysis(ContributeCore);
		}

		protected virtual AnalysisDescriptor ContributeCore(AnalysisDescriptor descriptor)
		{
			return Contribute(descriptor, Build());
		}

		protected abstract AnalysisDescriptor Contribute(AnalysisDescriptor descriptor, IEnumerable<KeyValuePair<string, T>> build);
	    public int CompareTo(IElasticsearchIndexCreationContributor other)
	    {
	        return other == null ? 0 : Order.CompareTo(other.Order);
	    }
	}
}
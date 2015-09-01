using System.Collections.Generic;

namespace Nest.Indexify.Contributors.Analysis
{
	public abstract class IndexAnalysisContributor<T> : ElasticsearchIndexCreationContributor where T : IAnalysisSetting
	{
		protected abstract IEnumerable<KeyValuePair<string, T>> Build();

		protected IndexAnalysisContributor(int order = 0) : base(order) { }

		protected virtual bool CanContribute(KeyValuePair<string, T> setting, FluentDictionary<string, T> existing)
		{
			return !existing.ContainsKey(setting.Key);
		}

		public sealed override void ContributeCore(CreateIndexDescriptor descriptor)
		{
			descriptor.Analysis(ContributeCore);
		}

		protected virtual AnalysisDescriptor ContributeCore(AnalysisDescriptor descriptor)
		{
			return Contribute(descriptor, Build());
		}

		protected abstract AnalysisDescriptor Contribute(AnalysisDescriptor descriptor, IEnumerable<KeyValuePair<string, T>> build);
	}
}
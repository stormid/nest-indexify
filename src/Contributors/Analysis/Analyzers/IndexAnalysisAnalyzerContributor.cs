using System.Collections.Generic;
using System.Linq;

namespace Nest.Indexify.Contributors.Analysis.Analyzers
{
	public abstract class IndexAnalysisAnalyzerContributor : IndexAnalysisContributor<AnalyzerBase>
	{
		protected IndexAnalysisAnalyzerContributor(int order = 0) : base(order)
		{
		}

		protected override AnalysisDescriptor Contribute(AnalysisDescriptor descriptor, IEnumerable<KeyValuePair<string, AnalyzerBase>> build)
		{
			return descriptor.Analyzers(a =>
			{
				foreach (var item in build.Where(x => CanContribute(x, a)))
				{
					a.Add(item.Key, item.Value);
				}
				return a;
			});
		}
	}
}
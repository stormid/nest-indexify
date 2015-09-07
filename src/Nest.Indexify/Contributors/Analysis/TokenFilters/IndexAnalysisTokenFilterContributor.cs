using System.Collections.Generic;
using System.Linq;

namespace Nest.Indexify.Contributors.Analysis.TokenFilters
{
	public abstract class IndexAnalysisTokenFilterContributor : IndexAnalysisContributor<TokenFilterBase>
	{
		protected IndexAnalysisTokenFilterContributor(int order = 0) : base(order)
		{
		}

		protected override AnalysisDescriptor Contribute(AnalysisDescriptor descriptor, IEnumerable<KeyValuePair<string, TokenFilterBase>> build)
		{
			return descriptor.TokenFilters(a =>
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
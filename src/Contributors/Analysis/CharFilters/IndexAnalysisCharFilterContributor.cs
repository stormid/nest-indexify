using System.Collections.Generic;
using System.Linq;

namespace Nest.Indexify.Contributors.Analysis.CharFilters
{
	public abstract class IndexAnalysisCharFilterContributor : IndexAnalysisContributor<CharFilterBase>
	{
        protected IndexAnalysisCharFilterContributor(int order = 0) : base(order) { }

		protected override AnalysisDescriptor Contribute(AnalysisDescriptor descriptor, IEnumerable<KeyValuePair<string, CharFilterBase>> build)
		{
			return descriptor.CharFilters(a =>
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
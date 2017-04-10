using System.Collections.Generic;
using System.Linq;

namespace Nest.Indexify.Contributors.Analysis.Tokenizers
{
	public abstract class IndexAnalysisTokenizerContributor : IndexAnalysisContributor<TokenizerBase>
	{
		protected override AnalysisDescriptor Contribute(AnalysisDescriptor descriptor, IEnumerable<KeyValuePair<string, TokenizerBase>> build)
		{
			return descriptor.Tokenizers(a =>
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
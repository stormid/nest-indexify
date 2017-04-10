using Nest.Indexify.Contributors.Analysis.TokenFilters;

namespace Nest.Indexify.Contributors.Analysis.Phonetic
{
	public class PhoneticIndexAnalysisContributor : CompositeElasticsearchIndexCreationContributor
	{
		public PhoneticIndexAnalysisContributor(string name = "indexify_phonetic", int order = 0) : base(order)
		{
            Add(new PhoneticIndexTokenFilterContributor(name));
            Add(new PhoneticIndexAnalyzerContributor(name, name));
		}
	}
}
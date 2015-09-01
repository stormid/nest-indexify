using Nest.Indexify.Contributors.Analysis.TokenFilters;

namespace Nest.Indexify.Contributors.Analysis.Autocomplete
{
	public class AutoCompleteIndexAnalysisContributor : CompositeElasticsearchIndexCreationContributor
	{
		public AutoCompleteIndexAnalysisContributor(string analyzerName = "indexify_ac", string tokenFilterName = "indexify_ac")
		{
            Add(new EdgeNGramTokenFilterContributor(tokenFilterName, order: -1));
            Add(new AutoCompleteAnalyzerContributor(analyzerName, tokenFilterName));
        }
	}
}
using Nest.Indexify.Contributors.Analysis.TokenFilters;

namespace Nest.Indexify.Contributors.Autocomplete
{
	public class AutoCompleteIndexAnalysisContributor : CompositeElasticsearchIndexCreationContributor
	{
		public AutoCompleteIndexAnalysisContributor(string analyzerName, string tokenFilterName)
		{
            Add(new EdgeNGramTokenFilterContributor(tokenFilterName));
            Add(new AutoCompleteAnalyzerContributor(analyzerName, tokenFilterName));
        }
	}
}
namespace Nest.Indexify.Contributors.Analysis.Languages
{
    public class EnglishIndexAnalysisContributor : IndexLanguageAnalysisContributor
    {
	    private readonly string[] _stopwords;

	    public EnglishIndexAnalysisContributor(string name = "indexify_english", int order = 0, params string[] stopwords): base(name, Language.English, order)
	    {
	        _stopwords = stopwords;
	    }

        protected override void BuildLanguageAnalyzer(LanguageAnalyzer languageAnalyzer)
        {
            languageAnalyzer.StopWords = _stopwords;
        }
    }
}
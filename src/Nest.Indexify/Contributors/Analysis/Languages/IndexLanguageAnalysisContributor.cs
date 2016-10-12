using System.Collections.Generic;
using Nest.Indexify.Contributors.Analysis.Analyzers;

namespace Nest.Indexify.Contributors.Analysis.Languages
{
    public class IndexLanguageAnalysisContributor : IndexAnalysisAnalyzerContributor
    {
        protected string Name { get; }
        protected Language Language { get; }

        public IndexLanguageAnalysisContributor(string name, Nest.Language language, int order): base(order)
        {
            Name = name;
            Language = language;
        }

        protected sealed override IEnumerable<KeyValuePair<string, AnalyzerBase>> Build()
        {
            var languageAnalyzer = new LanguageAnalyzer(Language);
            BuildLanguageAnalyzerCore(languageAnalyzer);
            yield return new KeyValuePair<string, AnalyzerBase>(Name, languageAnalyzer);
        }

        private void BuildLanguageAnalyzerCore(LanguageAnalyzer languageAnalyzer)
        {
            BuildLanguageAnalyzer(languageAnalyzer);
        }

        protected virtual void BuildLanguageAnalyzer(LanguageAnalyzer languageAnalyzer)
        {
        }
    }
}
using System.Collections.Generic;
using Nest.Indexify.Contributors.Analysis.Analyzers;

namespace Nest.Indexify.Contributors.Analysis.English
{
	public class EnglishIndexAnalysisContributor : IndexAnalysisAnalyzerContributor
	{
		private readonly string _name;

		public EnglishIndexAnalysisContributor(string name = "indexify_english", int order = 0): base(order)
		{
			_name = name;
		}

		protected override IEnumerable<KeyValuePair<string, AnalyzerBase>> Build()
		{
			yield return new KeyValuePair<string, AnalyzerBase>(_name, new LanguageAnalyzer(Language.English));
		}
	}
}
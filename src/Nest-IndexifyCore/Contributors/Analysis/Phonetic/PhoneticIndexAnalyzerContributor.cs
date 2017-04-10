using System.Collections.Generic;
using Nest.Indexify.Contributors.Analysis.Analyzers;

namespace Nest.Indexify.Contributors.Analysis.Phonetic
{
	public class PhoneticIndexAnalyzerContributor : IndexAnalysisAnalyzerContributor
	{
		private readonly string _name;
		private readonly string _tokenFilter;

		public PhoneticIndexAnalyzerContributor(string name, string tokenFilter, int order = 0) : base(order)
		{
			_name = name;
			_tokenFilter = tokenFilter;
		}

        protected override bool CanContributeCore(ICreateIndexRequest indexRequest, IElasticClient client)
        {
	        return indexRequest.Settings.Analysis.TokenFilters.ContainsKey(_tokenFilter);
	    }

	    protected override IEnumerable<KeyValuePair<string, AnalyzerBase>> Build()
		{
			yield return new KeyValuePair<string, AnalyzerBase>(_name, new CustomAnalyzer
			{
				Tokenizer = new StandardTokenizer().Type,
				Filter = new[] {new LowercaseTokenFilter().Type, _tokenFilter}
			});
		}
	}
}
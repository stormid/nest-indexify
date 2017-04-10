using System.Collections.Generic;

namespace Nest.Indexify.Contributors.Analysis.TokenFilters
{
	public class EdgeNGramTokenFilterContributor : IndexAnalysisTokenFilterContributor
	{
		private readonly string _name;
		private readonly EdgeNGramSide _side;
		private readonly int _minGram;
		private readonly int _maxGram;

		public EdgeNGramTokenFilterContributor(string name, EdgeNGramSide side = EdgeNGramSide.Front, int minGram = 3, int maxGram = 10, int order = 0) : base(order)
		{
			_name = name;
			_side = side;
			_minGram = minGram;
			_maxGram = maxGram;
		}

		protected override IEnumerable<KeyValuePair<string, TokenFilterBase>> Build()
		{
			yield return new KeyValuePair<string, TokenFilterBase>(_name, new EdgeNGramTokenFilter
			{
				Side = _side,
				MinGram = _minGram,
				MaxGram = _maxGram
			});
		}
	}
}
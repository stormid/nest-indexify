using System.Collections.Generic;

namespace Nest.Indexify.Contributors.Analysis.TokenFilters
{
	public class PhoneticIndexTokenFilterContributor : IndexAnalysisTokenFilterContributor
	{
		private readonly string _name;
		private readonly string _encoder;

		public PhoneticIndexTokenFilterContributor(string name, string encoder = "metaphone")
		{
			_name = name;
			_encoder = encoder;
		}

		protected override IEnumerable<KeyValuePair<string, TokenFilterBase>> Build()
		{
			yield return new KeyValuePair<string, TokenFilterBase>(_name, new PhoneticTokenFilter
			{
				Encoder = _encoder
			});
		}
	}
}
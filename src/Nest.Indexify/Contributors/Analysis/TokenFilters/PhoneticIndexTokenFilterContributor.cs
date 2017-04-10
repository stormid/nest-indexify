using System.Collections.Generic;

namespace Nest.Indexify.Contributors.Analysis.TokenFilters
{
	public class PhoneticIndexTokenFilterContributor : IndexAnalysisTokenFilterContributor
	{
		private readonly string _name;
		private readonly PhoneticEncoder _encoder;

		public PhoneticIndexTokenFilterContributor(string name, PhoneticEncoder encoder = PhoneticEncoder.Metaphone)
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
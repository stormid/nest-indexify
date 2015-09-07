using System.Collections.Generic;

namespace Nest.Indexify.Contributors.Analysis.CharFilters
{
    public class PatternReplaceCharFilterContributor : IndexAnalysisCharFilterContributor
    {
        private readonly string _pattern;
        private readonly string _replacement;
        private readonly string _name;

        public PatternReplaceCharFilterContributor(string pattern, string replacement, string name = "indexify_pattern", int order = 0) : base(order)
        {
            _pattern = pattern;
            _replacement = replacement;
            _name = name;
        }

        protected override IEnumerable<KeyValuePair<string, CharFilterBase>> Build()
        {
            yield return new KeyValuePair<string, CharFilterBase>(_name, new PatternReplaceCharFilter()
            {
                Pattern = _pattern,
                Replacement = _replacement
            });
        }
    }
}
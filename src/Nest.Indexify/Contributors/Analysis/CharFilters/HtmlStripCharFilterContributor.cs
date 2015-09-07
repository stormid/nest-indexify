using System.Collections.Generic;

namespace Nest.Indexify.Contributors.Analysis.CharFilters
{
    public class HtmlStripCharFilterContributor : IndexAnalysisCharFilterContributor {
        private readonly string _name;

        public HtmlStripCharFilterContributor(string name = "indexify_htmlstrip", int order = 0) : base(order)
        {
            _name = name;
        }

        protected override IEnumerable<KeyValuePair<string, CharFilterBase>> Build()
        {
            yield return new KeyValuePair<string, CharFilterBase>(_name, new HtmlStripCharFilter());
        }
    }
}
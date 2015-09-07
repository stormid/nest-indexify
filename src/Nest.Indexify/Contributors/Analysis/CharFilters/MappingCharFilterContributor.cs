using System.Collections.Generic;

namespace Nest.Indexify.Contributors.Analysis.CharFilters
{
    public class MappingCharFilterContributor : IndexAnalysisCharFilterContributor
    {
        private readonly IEnumerable<string> _mappings;
        private readonly string _mappingPath;
        private readonly string _name;

        public MappingCharFilterContributor(IEnumerable<string> mappings, string mappingPath = null, string name = "indexify_charmapping", int order = 0) : base(order)
        {
            _mappings = mappings;
            _mappingPath = mappingPath;
            _name = name;
        }

        protected override IEnumerable<KeyValuePair<string, CharFilterBase>> Build()
        {
            yield return new KeyValuePair<string, CharFilterBase>(_name, new MappingCharFilter()
            {
                Mappings = _mappings,
                MappingsPath = _mappingPath
            });
        }
    }
}
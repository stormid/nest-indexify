using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nest.Indexify.Contributors.Mapping
{
    public abstract class IndexMappingContributor<TType> : ElasticsearchIndexCreationContributor where TType : class
    {
        private static readonly IDictionary<string, string> CoreMetadata = new Dictionary<string, string>()
        {
            { "_clrType", typeof(TType).AssemblyQualifiedName },
            {"_libVer", typeof (IElasticsearchIndexCreationContributor).GetTypeInfo().Assembly.GetName().Version.ToString() }
        };

        protected IndexMappingContributor(int order = 99)  : base(order) { }

        public sealed override void ContributeCore(CreateIndexDescriptor descriptor, IElasticClient client)
        {
            descriptor.Mappings(m => m.Map<TType>(MappingCore));
        }

        protected virtual TypeMappingDescriptor<TType> Mapping(TypeMappingDescriptor<TType> descriptor)
        {
            return descriptor;
        }

        protected TypeMappingDescriptor<TType> MappingCore(TypeMappingDescriptor<TType> descriptor)
        {
            ModifyMetadataCore(descriptor);
            return Mapping(descriptor);
        }

        protected void ModifyMetadataCore(TypeMappingDescriptor<TType> descriptor)
        {
            var additionalMetadata = ModifyMetadata();
            descriptor.Meta(m =>
            {
                foreach (var metadata in CoreMetadata.Union(additionalMetadata))
                {
                    m.Add(metadata.Key, metadata.Value);
                }
                return m;
            });
        }

        protected virtual IEnumerable<KeyValuePair<string, string>> ModifyMetadata()
        {
            return Enumerable.Empty<KeyValuePair<string, string>>();
        }
    }
}
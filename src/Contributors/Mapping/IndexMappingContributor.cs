using System.Collections.Generic;
using System.Linq;

namespace Nest.Indexify.Contributors.Mapping
{
    public abstract class IndexMappingContributor<TType> : ElasticsearchIndexCreationContributor where TType : class
    {
        private static readonly IDictionary<string, string> CoreMetadata = new Dictionary<string, string>()
        {
            { "_clrType", typeof(TType).AssemblyQualifiedName },
            {"_libVer", typeof (IElasticsearchIndexCreationContributor).Assembly.GetName().Version.ToString() }
        };

        protected IndexMappingContributor(int order = 99)  : base(order) { }

        public sealed override void Contribute(CreateIndexDescriptor descriptor)
        {
            descriptor.AddMapping<TType>(MappingCore);
        }

        protected virtual PutMappingDescriptor<TType> Mapping(PutMappingDescriptor<TType> descriptor)
        {
            return descriptor;
        }

        protected PutMappingDescriptor<TType> MappingCore(PutMappingDescriptor<TType> descriptor)
        {
            ModifyMetadataCore(descriptor);
            return Mapping(descriptor);
        }

        protected void ModifyMetadataCore(PutMappingDescriptor<TType> descriptor)
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
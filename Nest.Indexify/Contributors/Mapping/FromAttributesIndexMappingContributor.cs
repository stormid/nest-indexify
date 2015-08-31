using System.Collections.Generic;

namespace Nest.Indexify.Contributors.Mapping
{
	public class FromAttributesIndexMappingContributor<TType> : ElasticsearchIndexCreationContributor where TType : class
	{
		public FromAttributesIndexMappingContributor(int order = 0)
		{
			Order = order;
		}

		public override void Contribute(CreateIndexDescriptor descriptor)
		{
			descriptor.AddMapping<TType>(m =>
			{
				m = m.MapFromAttributes();
				return MappingCore(m);
			});
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

		protected virtual void ModifyMetadataCore(PutMappingDescriptor<TType> descriptor)
		{
			descriptor.Meta(m =>
			{
				m.Add("_clrType", typeof(TType).AssemblyQualifiedName);
				ModifyMetadata(m);
				return m;
			});
		}

		protected virtual void ModifyMetadata(IDictionary<string, object> metadata)
		{
		}
	}
}

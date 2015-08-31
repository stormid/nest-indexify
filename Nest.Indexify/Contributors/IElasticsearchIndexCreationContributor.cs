using System;

namespace Nest.Indexify.Contributors
{
    public interface IElasticsearchIndexCreationContributor : IComparable<IElasticsearchIndexCreationContributor>
    {
		int Order { get; }
        void Contribute(CreateIndexDescriptor descriptor);
    }
}
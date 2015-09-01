using System;

namespace Nest.Indexify.Contributors
{
    public interface IElasticsearchIndexCreationContributor : IComparable<IElasticsearchIndexCreationContributor>
    {
		int Order { get; }
        bool CanContribute(ICreateIndexRequest indexRequest);
        void Contribute(CreateIndexDescriptor descriptor);
    }
}
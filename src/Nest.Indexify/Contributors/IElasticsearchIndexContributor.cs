using System;

namespace Nest.Indexify.Contributors
{
    public interface IElasticsearchIndexContributor : IComparable<IElasticsearchIndexContributor>
    {
        int Order { get; }
    }
}
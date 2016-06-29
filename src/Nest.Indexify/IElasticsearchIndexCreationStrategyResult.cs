using System;
using System.Collections.Generic;
using Nest.Indexify.Contributors;

namespace Nest.Indexify
{
    public interface IElasticsearchIndexCreationStrategyResult
    {
        IEnumerable<IElasticsearchIndexContributor> Contributors { get; }
        bool Success { get; }
        IIndicesOperationResponse IndexResponse { get; }
        Exception Exception { get; }
    }
}
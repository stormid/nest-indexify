using System;
using System.Collections.Generic;
using Nest.Indexify.Contributors;

namespace Nest.Indexify
{
    public class ElasticsearchIndexCreationStrategyResult : IElasticsearchIndexCreationStrategyResult
    {
        public ElasticsearchIndexCreationStrategyResult(IEnumerable<IElasticsearchIndexContributor> contributors, bool success, IIndicesOperationResponse indexResponse, Exception exception)
        {
            Contributors = contributors;
            Success = success;
            IndexResponse = indexResponse;
            Exception = exception;
        }

        public IEnumerable<IElasticsearchIndexContributor> Contributors { get; }
        public bool Success { get; }
        public IIndicesOperationResponse IndexResponse { get; }
        public Exception Exception { get; }
    }
}
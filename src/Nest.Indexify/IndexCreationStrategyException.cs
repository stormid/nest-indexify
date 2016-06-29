using System;
using System.Runtime.Serialization;

namespace Nest.Indexify
{
    [Serializable]
    public class IndexCreationStrategyException : Exception
    {
        public IndexCreationStrategyException()
        {
        }

        public IndexCreationStrategyException(string message) : base(message)
        {
        }

        public IndexCreationStrategyException(string message, Exception inner) : base(message, inner)
        {
        }

        protected IndexCreationStrategyException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
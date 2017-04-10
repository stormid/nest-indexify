using System;

namespace Nest.Indexify
{
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
    }
}
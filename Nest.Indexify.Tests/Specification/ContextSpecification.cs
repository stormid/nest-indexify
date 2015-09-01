using System.Diagnostics;
using Xunit;

namespace Nest.Indexify.Tests.Specification
{
    public abstract class ContextSpecification
    {
        internal ContextSpecification(bool deferSetup = false)
        {
            if(!deferSetup) Setup();
        }

        protected ContextSpecification() : this(false)
        {
            
        }

        protected virtual void Setup()
        {
            Context();
            Because();
        }

        protected virtual void Context()
        {
            
        }

        protected virtual void Because()
        {
            Debug.WriteLine("WARNING: No Because() defined");
        }
    }

    public abstract class ContextSpecification<TSharedContext> : ContextSpecification, IClassFixture<TSharedContext> where TSharedContext : class, new()
    {
        protected ContextSpecification(TSharedContext context) : base(true)
        {
            SharedContext = context;
            Setup();
        }

        protected ContextSpecification() : this(new TSharedContext())
        {
        }

        protected TSharedContext SharedContext { get; private set; }
    }
}
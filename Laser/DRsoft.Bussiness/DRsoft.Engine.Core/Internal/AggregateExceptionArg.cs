using System;

namespace DRsoft.Engine.Core.Internal
{
    public class AggregateExceptionArg : EventArgs
    {
        public AggregateException AggregateException { get; set; }
    }
}

using System;

namespace kaizen.domain.@base.exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public readonly Guid Id;

        public AggregateNotFoundException(Guid id) : base(
            $"There were no events discovered for the requested aggregate ({id})")
        {
            Id = id;
        }
    }
}
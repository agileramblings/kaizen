using System;

namespace kaizen.domain.@base.persistence
{
    public abstract class ReadModelBase
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
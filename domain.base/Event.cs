using System;

namespace kaizen.domain.@base
{
    public class Event : IMessage
    {
        public Guid Id = Guid.NewGuid();
        public int Version;
    }
}
using System;
using System.Collections.Generic;

namespace kaizen.domain.@base
{
    public class Event : IMessage
    {
        public Guid Id;
        public int Version;
    }
}
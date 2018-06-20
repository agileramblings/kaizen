using System;
using System.Collections.Generic;

namespace kaizen.domain.@base.events.storage
{
    public interface IEventDescriptorStorage
    {
        bool GetEventDescriptors(Guid aggregateId, out List<EventDescriptor> eventDescriptors);
        void AddDescriptor(EventDescriptor ed);
        void AddDescriptors(IEnumerable<EventDescriptor> eds);
    }
}
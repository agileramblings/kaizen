using System;

namespace kaizen.domain.@base.events.storage
{
    public class EventDescriptorEntity
    {
        public EventDescriptorEntity()
        {
        }

        public EventDescriptorEntity(Guid aggregateId, int version)
        {
        }

        public string EventData { get; set; }
        public string EventType { get; set; }
    }
}
using System;
using kaizen.domain.@base;
using Newtonsoft.Json;

namespace kaizen.domain.@base.events.storage
{
    public class EventFactory
    {
        public static Event GetConcreteEvent(EventDescriptorEntity ede)
        {
            var t = Type.GetType(ede.EventType);
            return JsonConvert.DeserializeObject(ede.EventData, t) as Event;
        }
    }
}
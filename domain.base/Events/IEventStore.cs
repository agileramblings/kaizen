using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kaizen.domain.@base;

namespace kaizen.domain.@base.events
{
    public interface IEventStore
    {
        Task Put(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        Task<IEnumerable<Event>> GetEventsForAggregate(Guid aggregateId);
    }
}
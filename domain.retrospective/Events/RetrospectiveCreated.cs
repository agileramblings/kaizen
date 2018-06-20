using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class RetrospectiveCreated : Event
    {
        public DateTime CreatedOn;
        public string Owner;

        public RetrospectiveCreated(Guid newRetrospectiveId, string owner)
        {
            Id = newRetrospectiveId;
            CreatedOn = DateTime.UtcNow;
            Owner = owner;
        }
    }
}
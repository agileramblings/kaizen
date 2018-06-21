using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ActionItemUpdated : Event
    {
        public Guid RetrospectiveId;
        public Guid ActionItemIdentifier;
        public string Description;

        public ActionItemUpdated(Guid retrospectiveId, Guid actionItemIdentifier, string description)
        {
            RetrospectiveId = retrospectiveId;
            ActionItemIdentifier = actionItemIdentifier;
            Description = description;
        }
    }
}
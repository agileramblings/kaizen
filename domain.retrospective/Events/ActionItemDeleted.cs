using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ActionItemDeleted : Event
    {
        public Guid ActionItemIdentifier;
        public Guid RetrospectiveId;

        public ActionItemDeleted(Guid retrospectiveId, Guid actionItemIdentifier)
        {
            RetrospectiveId = retrospectiveId;
            ActionItemIdentifier = actionItemIdentifier;
        }
    }
}
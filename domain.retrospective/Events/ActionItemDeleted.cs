using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ActionItemDeleted : Event
    {
        public Guid ActionItemIdentifier;

        public ActionItemDeleted(Guid actionItemIdentifier)
        {
            ActionItemIdentifier = actionItemIdentifier;
        }
    }
}
using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ActionItemUpdated : Event
    {
        public Guid ActionItemIdentifier;
        public string Description;

        public ActionItemUpdated(Guid actionItemIdentifier, string description)
        {
            ActionItemIdentifier = actionItemIdentifier;
            Description = description;
        }
    }
}
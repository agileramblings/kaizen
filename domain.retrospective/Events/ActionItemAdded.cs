using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ActionItemAdded : Event
    {
        public string Description;
        public Guid ActionItemId;
        public string ParticipantId;
        public Guid RetrospectiveId;

        public ActionItemAdded(Guid retrospectiveId, Guid actionItemId, string description, string participantId)
        {
            RetrospectiveId = retrospectiveId;
            ActionItemId = actionItemId;
            Description = description;
            ParticipantId = participantId;
        }
    }
}

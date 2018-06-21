using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ActionItemAdded : Event
    {
        public string Description;
        public string ParticipantId;
        public Guid RetrospectiveId;

        public ActionItemAdded(Guid retrospectiveId, string description, string participantId)
        {
            RetrospectiveId = retrospectiveId;
            Description = description;
            ParticipantId = participantId;
        }
    }
}

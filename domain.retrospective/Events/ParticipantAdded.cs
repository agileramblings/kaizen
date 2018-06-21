using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ParticipantAdded : Event
    {
        public Guid RetrospectiveId;
        public string ParticipantIdentifier;

        public ParticipantAdded(Guid retrospectiveId, string participantId)
        {
            RetrospectiveId = retrospectiveId;
            ParticipantIdentifier = participantId;
        }
    }
}
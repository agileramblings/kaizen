using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class DislikeVoteToggled : Event
    {
        public Guid RetrospectiveId;
        public Guid DislikeIdentifier;
        public string ParticipantId;

        public DislikeVoteToggled(Guid retrospectiveId, Guid dislikeIdentifier, string participantId)
        {
            RetrospectiveId = retrospectiveId;
            DislikeIdentifier = dislikeIdentifier;
            ParticipantId = participantId;
        }
    }
}
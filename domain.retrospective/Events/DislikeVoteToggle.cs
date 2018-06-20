using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class DislikeVoteToggled : Event
    {
        public Guid DislikeIdentifier;
        public string ParticipantId;

        public DislikeVoteToggled(Guid dislikeIdentifier, string participantId)
        {
            DislikeIdentifier = dislikeIdentifier;
            ParticipantId = participantId;
        }
    }
}
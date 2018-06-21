using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class LikeVoteToggled : Event
    {
        public Guid RetrospectiveId;
        public Guid LikeIdentifier;
        public string ParticipantId;

        public LikeVoteToggled(Guid retrospectiveId, Guid likeIdentifier, string participantId)
        {
            RetrospectiveId = retrospectiveId;
            LikeIdentifier = likeIdentifier;
            ParticipantId = participantId;
        }
    }
}

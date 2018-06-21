using System;
using System.Collections.Generic;
using System.Text;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class LikeVoteToggled : Event
    {
        public Guid RetrospectiveId;
        public Guid LikeIdentifier;
        public string ParticipantId;

        public LikeVoteToggled(Guid likeIdentifier, string participantId)
        {
            LikeIdentifier = likeIdentifier;
            ParticipantId = participantId;
        }
    }
}

using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class LikeAdded : Event
    {
        public Guid RetrospectiveId;
        public Guid LikeId;
        public string Description;
        public string ParticipantId;

        public LikeAdded(Guid retrospectiveId, Guid likeId, string description, string participantId)
        {
            RetrospectiveId = retrospectiveId;
            LikeId = likeId;
            Description = description;
            ParticipantId = participantId;
        }
    }
}
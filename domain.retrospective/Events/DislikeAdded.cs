using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class DislikeAdded : Event
    {
        public Guid RetrospectiveId;
        public Guid DislikeId;
        public string Description;
        public string ParticipantId;

        public DislikeAdded(Guid retrospectiveId, Guid dislikeId, string description, string participantId)
        {
            RetrospectiveId = retrospectiveId;
            DislikeId = dislikeId;
            Description = description;
            ParticipantId = participantId;
        }
    }
}
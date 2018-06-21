using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class LikeAdded : Event
    {
        public Guid RetrospectiveId;
        public string Description;
        public string ParticipantId;

        public LikeAdded(string description, string participantId)
        {
            Description = description;
            ParticipantId = participantId;
        }
    }
}
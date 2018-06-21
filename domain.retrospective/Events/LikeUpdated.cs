using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class LikeUpdated : Event
    {
        public Guid RetrospectiveId;
        public string Description;
        public Guid LikeIdentifier;

        public LikeUpdated(Guid retrospectiveId, Guid likeIdentifier, string description)
        {
            RetrospectiveId = retrospectiveId;
            LikeIdentifier = likeIdentifier;
            Description = description;
        }
    }
}
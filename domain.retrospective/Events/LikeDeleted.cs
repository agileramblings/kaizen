using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class LikeDeleted : Event
    {
        public Guid RetrospectiveId;
        public Guid LikeIdentifier;

        public LikeDeleted(Guid retrospectiveId, Guid likeIdentifier)
        {
            RetrospectiveId = retrospectiveId;
            LikeIdentifier = likeIdentifier;
        }
    }
}
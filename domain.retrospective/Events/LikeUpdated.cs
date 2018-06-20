using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class LikeUpdated : Event
    {
        public string Description;
        public Guid LikeIdentifier;

        public LikeUpdated(Guid likeIdentifier, string description)
        {
            LikeIdentifier = likeIdentifier;
            Description = description;
        }
    }
}
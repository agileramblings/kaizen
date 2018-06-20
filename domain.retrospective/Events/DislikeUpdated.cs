using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class DislikeUpdated : Event
    {
        public string Description;
        public Guid DislikeIdentifier;

        public DislikeUpdated(Guid dislikeIdentifier, string description)
        {
            Description = description;
            DislikeIdentifier = dislikeIdentifier;
        }
    }
}
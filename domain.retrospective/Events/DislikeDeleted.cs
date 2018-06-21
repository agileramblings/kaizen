using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class DislikeDeleted : Event
    {
        public Guid RetrospectiveId;
        public Guid DislikeIdentifier;

        public DislikeDeleted(Guid dislikeIdentifier)
        {
            DislikeIdentifier = dislikeIdentifier;
        }
    }
}
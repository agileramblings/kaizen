using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    public class UpdateDislike : CommandBase
    {
        public readonly string Description;
        public readonly string ParticipantId;
        public readonly Guid RetrospectiveId;
        public readonly Guid DislikeId;

        public UpdateDislike(Guid retrospectiveId, Guid dislikeId, string description, string participantId)
            : base(Guid.NewGuid(), -1)
        {
            RetrospectiveId = retrospectiveId;
            DislikeId = dislikeId;
            Description = description;
            ParticipantId = participantId;
        }
    }
}

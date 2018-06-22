using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    // Commands that allow you to alter the state of the system
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
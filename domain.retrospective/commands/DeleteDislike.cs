using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    public class DeleteDislike : CommandBase
    {
        public readonly string ParticipantId;
        public readonly Guid RetrospectiveId;
        public readonly Guid DislikeId;

        public DeleteDislike(Guid retrospectiveId, Guid dislikeId, string participantId)
            : base(Guid.NewGuid(), -1)
        {
            RetrospectiveId = retrospectiveId;
            DislikeId = dislikeId;
            ParticipantId = participantId;
        }
    }
}

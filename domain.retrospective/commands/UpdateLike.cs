using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    // Commands that allow you to alter the state of the system
    public class UpdateLike : CommandBase
    {
        public readonly string Description;
        public readonly string ParticipantId;
        public readonly Guid RetrospectiveId;
        public readonly Guid LikeId;

        public UpdateLike(Guid retrospectiveId, Guid likeId, string description, string participantId)
            : base(Guid.NewGuid(), -1)
        {
            RetrospectiveId = retrospectiveId;
            LikeId = likeId;
            Description = description;
            ParticipantId = participantId;
        }
    }
}
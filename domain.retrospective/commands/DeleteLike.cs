using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    public class DeleteLike : CommandBase
    {
        public readonly string ParticipantId;
        public readonly Guid RetrospectiveId;
        public readonly Guid LikeId;

        public DeleteLike(Guid retrospectiveId, Guid likeId, string participantId)
            : base(Guid.NewGuid(), -1)
        {
            RetrospectiveId = retrospectiveId;
            LikeId = likeId;
            ParticipantId = participantId;
        }
    }
}

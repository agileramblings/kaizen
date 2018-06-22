using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    public class DeleteActionItem : CommandBase
    {
        public readonly string ParticipantId;
        public readonly Guid RetrospectiveId;
        public readonly Guid ActionItemId;

        public DeleteActionItem(Guid retrospectiveId, Guid actionItemId, string participantId)
            : base(Guid.NewGuid(), -1)
        {
            RetrospectiveId = retrospectiveId;
            ActionItemId = actionItemId;
            ParticipantId = participantId;
        }
    }
}

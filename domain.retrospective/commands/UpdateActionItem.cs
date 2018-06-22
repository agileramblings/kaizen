using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    public class UpdateActionItem : CommandBase
    {
        public readonly string Description;
        public readonly string ParticipantId;
        public readonly Guid RetrospectiveId;
        public readonly Guid ActionItemId;

        public UpdateActionItem(Guid retrospectiveId, Guid actionItemId, string description, string participantId)
            : base(Guid.NewGuid(), -1)
        {
            RetrospectiveId = retrospectiveId;
            ActionItemId = actionItemId;
            Description = description;
            ParticipantId = participantId;
        }
    }
}

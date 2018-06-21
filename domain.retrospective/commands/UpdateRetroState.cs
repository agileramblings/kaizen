using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    public class UpdateRetroState : CommandBase
    {
        public readonly string ParticipantId;
        public readonly Guid RetrospectiveId;
        public readonly RetrospectiveState State;

        public UpdateRetroState(Guid retrospectiveId, string participantId, RetrospectiveState state)
            : base(Guid.NewGuid(), -1)
        {
            RetrospectiveId = retrospectiveId;
            ParticipantId = participantId;
            State = state;
        }
    }
}

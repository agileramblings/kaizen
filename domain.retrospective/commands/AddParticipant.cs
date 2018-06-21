using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    // Commands that allow you to alter the state of the system
    public class AddParticipant : CommandBase
    {
        public readonly Guid RetrospectiveId;
        public readonly string ParticipantId;

        public AddParticipant(Guid retrospectiveId, string participantId)
            : base(Guid.NewGuid(), -1)
        {
            RetrospectiveId = retrospectiveId;
            ParticipantId = participantId;
        }
    }
}
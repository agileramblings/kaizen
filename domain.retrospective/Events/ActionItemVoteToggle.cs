using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ActionItemVoteToggled : Event
    {
        public Guid ActionItemIdentifier;
        public string ParticipantId;

        public ActionItemVoteToggled(Guid actionItemIdentifier, string participantId)
        {
            ActionItemIdentifier = actionItemIdentifier;
            ParticipantId = participantId;
        }
    }
}
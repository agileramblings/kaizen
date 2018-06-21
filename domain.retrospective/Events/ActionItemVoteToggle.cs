using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class ActionItemVoteToggled : Event
    {
        public Guid RetrospectiveId;
        public Guid ActionItemIdentifier;
        public string ParticipantId;

        public ActionItemVoteToggled(Guid retrospectiveId, Guid actionItemIdentifier, string participantId)
        {
            RetrospectiveId = retrospectiveId;
            ActionItemIdentifier = actionItemIdentifier;
            ParticipantId = participantId;
        }
    }
}
using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class RetrospectiveStateChanged : Event
    {
        public Guid RetrospectiveId;
        public RetrospectiveState TargetState;

        public RetrospectiveStateChanged(Guid retrospectiveId, RetrospectiveState targetState)
        {
            RetrospectiveId = retrospectiveId;
            TargetState = targetState;
        }
    }
}
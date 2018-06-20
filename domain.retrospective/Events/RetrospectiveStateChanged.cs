using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.events
{
    public class RetrospectiveStateChanged : Event
    {
        public RetrospectiveState TargetState;
        public RetrospectiveStateChanged(RetrospectiveState targetState)
        {
            TargetState = targetState;
        }
    }
}
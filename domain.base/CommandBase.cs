using System;

namespace kaizen.domain.@base
{
    public abstract class CommandBase : Command
    {
        public readonly Guid CommandId;
        public readonly int CurrentAggregateVersion;

        protected CommandBase(Guid commandId, int currentAggregateVersion)
        {
            CommandId = commandId;
            CurrentAggregateVersion = currentAggregateVersion;
        }
    }
}
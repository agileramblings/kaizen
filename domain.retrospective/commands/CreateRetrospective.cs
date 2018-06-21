using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    // Commands that allow you to alter the state of the system
    public class CreateRetrospective : CommandBase
    {
        public readonly string Owner;
        public readonly Guid RetrospectiveId;

        public CreateRetrospective(Guid retrospectiveId, string owner)
            : base(Guid.NewGuid(), -1)
        {
            Owner = owner;
            RetrospectiveId = retrospectiveId;
        }
    }
}
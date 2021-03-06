﻿using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    // Commands that allow you to alter the state of the system
    public class AddLike : CommandBase
    {
        public readonly string Description;
        public readonly string ParticipantId;
        public readonly Guid RetrospectiveId;

        public AddLike(Guid retrospectiveId, string description, string participantId)
            : base(Guid.NewGuid(), -1)
        {
            RetrospectiveId = retrospectiveId;
            Description = description;
            ParticipantId = participantId;
        }
    }
}
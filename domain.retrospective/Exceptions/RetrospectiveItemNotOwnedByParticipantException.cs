using System;
using System.Collections.Generic;
using System.Text;

namespace kaizen.domain.retrospective.exceptions
{
    public class RetrospectiveItemNotOwnedByParticipantException : Exception
    {
        public string ItemType;
        public Guid ItemIdentifier;
        public string ParticipantId;

        public RetrospectiveItemNotOwnedByParticipantException(string itemType, Guid itemIdentifier, string participantId)
        {
            ItemType = itemType;
            ItemIdentifier = itemIdentifier;
            ParticipantId = participantId;
        }
    }
}

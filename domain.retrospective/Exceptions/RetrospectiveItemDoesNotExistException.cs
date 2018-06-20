using System;
using System.Collections.Generic;
using System.Text;

namespace kaizen.domain.retrospective.exceptions
{
    public class RetrospectiveItemDoesNotExistException : Exception
    {
        public string ItemType;
        public Guid ItemIdentifier;

        public RetrospectiveItemDoesNotExistException(string itemType, Guid itemIdentifier)
        {
            ItemType = itemType;
            ItemIdentifier = itemIdentifier;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace kaizen.domain.retrospective
{
    public abstract class RetrospectiveItemBase
    {
        public string Description { get; set; }
        public string ParticipantId { get; set; }
        public Guid Id { get; set; }
    }
}

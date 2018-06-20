using System;
using System.Collections.Generic;
using System.Text;

namespace kaizen.domain.retrospective
{
    public abstract class RetrospectiveItemBase
    {
        private readonly HashSet<string> _voters = new HashSet<string>();

        public string Description { get; set; }
        public string ParticipantId { get; set; }
        public Guid Id { get; set; }

        public int Votes => _voters.Count;

        public void ToggleVote(string participantId)
        {
            if (_voters.Contains(participantId))
            {
                _voters.Remove(participantId);
            }
            else
            {
                _voters.Add(participantId);
            }
        }
    }
}

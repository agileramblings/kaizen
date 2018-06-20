using System;
using System.Collections.Generic;

namespace kaizen.domain.retrospective
{
    public class Like : RetrospectiveItemBase
    {
        private readonly HashSet<string> _voters = new HashSet<string>();
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
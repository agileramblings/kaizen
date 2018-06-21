using System;
using kaizen.domain.@base;

namespace kaizen.domain.retrospective.commands
{
    public class Vote : CommandBase
    {
        public readonly string ParticipantId;
        public readonly Guid RetrospectiveId;
        public readonly Guid ItemId;
        public readonly VoteItemType ItemVoteType;

        public Vote(Guid retrospectiveId, Guid itemId, VoteItemType voteItemType, string participantId)
            : base(Guid.NewGuid(), -1)
        {
            RetrospectiveId = retrospectiveId;
            ParticipantId = participantId;
            ItemId = itemId;
            ItemVoteType = voteItemType;
        }

        public enum VoteItemType
        {
            Like,
            DisLike,
            ActionItem
        }
    }
}

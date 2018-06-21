using System;
using System.Collections.Generic;
using kaizen.domain.@base.persistence;

namespace kaizen.domain.retrospective.readmodel
{
    public class RetrospectiveDetails : ReadModelBase
    {
        public RetrospectiveState State;
        public string Owner;
        public DateTime CreatedOn;
        public List<string> Participants;
        public List<Like> Likes;
        public List<Dislike> Dislikes;
        public List<ActionItem> ActionItems;

        public int LikesCount => Likes?.Count ?? 0;
        public int DislikesCount => Dislikes?.Count ?? 0;
        public int ActionItemsCount => ActionItems?.Count ?? 0;
    }
}

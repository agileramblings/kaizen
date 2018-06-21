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
        public List<string> Participants { get; set; } = new List<string>();
        public List<Like> Likes { get; set; } = new List<Like>();
        public List<Dislike> Dislikes { get; set; } = new List<Dislike>();
        public List<ActionItem> ActionItems { get; set; } = new List<ActionItem>();

        public int LikesCount => Likes?.Count ?? 0;
        public int DislikesCount => Dislikes?.Count ?? 0;
        public int ActionItemsCount => ActionItems?.Count ?? 0;
    }
}

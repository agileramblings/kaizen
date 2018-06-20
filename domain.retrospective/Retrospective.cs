using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using kaizen.domain.@base;
using kaizen.domain.retrospective.events;
using kaizen.domain.retrospective.exceptions;

// ReSharper disable UnusedMember.Local

namespace kaizen.domain.retrospective
{
    public class Retrospective : AggregateBase
    {
        public DateTime CreatedOn { get; private set; }
        public string Owner { get; private set; }
        public RetrospectiveState State { get; private set; } = RetrospectiveState.CollectingSuggestions;
        public List<string> Participants { get; } = new List<string>();
        public List<Like> Likes { get; } = new List<Like>();
        public List<Dislike> Dislikes { get; } = new List<Dislike>();
        public List<ActionItem> ActionItems { get; } = new List<ActionItem>();

        public Retrospective(Guid newRetrospectiveId, string owner)
        {
            ApplyChange(new RetrospectiveCreated(newRetrospectiveId, owner));
        }

        public void InviteAParticipant(string participant)
        {
            ApplyChange(new ParticipantAdded(participant));
        }
        public void StartCollectingActionItems(string participantId)
        {
            if (participantId != Owner)
            {
                throw new ParticipantCannotChangeRetrospectiveStateException();
            }
            ApplyChange(new RetrospectiveStateChanged(RetrospectiveState.CollectionActionItems));
        }

        public void AddLikeItem(string description, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectingSuggestions);
            ApplyChange(new LikeAdded(description, participantId));
        }

        public void UpdateLikeItem(Guid likeIdentifier, string description, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectingSuggestions);
            var like = Likes.FirstOrDefault(l => l.Id == likeIdentifier);
            if (like == null)
            {
                throw new RetrospectiveItemDoesNotExistException(nameof(Like), likeIdentifier);
            }

            if (like.ParticipantId != participantId)
            {
                throw new RetrospectiveItemNotOwnedByParticipantException(nameof(Like), likeIdentifier, participantId);
            }

            ApplyChange(new LikeUpdated(likeIdentifier, description));
        }

        public void AddDislikeItem(string description, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectingSuggestions);
            ApplyChange(new DislikeAdded(description, participantId));
        }
        public void AddActionItem(string description, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectionActionItems);
            ApplyChange(new ActionItemAdded(description, participantId));
        }

        #region Private Setters
        // Applied by Reflection when reading events (AggregateBase -> this.AsDynamic().Apply(@event);)
        // Ensures aggregates get their needed property values (e.g. Id) from events
        private void Apply(RetrospectiveCreated e)
        {
            Id = e.Id;
            CreatedOn = e.CreatedOn;
            Owner = e.Owner;
        }

        private void Apply(ParticipantAdded e)
        {
            Participants.Add(e.ParticipantIdentifier);
        }

        private void Apply(LikeAdded e)
        {
            Likes.Add(new Like{Id = e.Id, Description = e.Description, ParticipantId = e.ParticipantId});
        }

        private void Apply(DislikeAdded e)
        {
            Dislikes.Add(new Dislike { Id = e.Id, Description = e.Description, ParticipantId = e.ParticipantId });
        }

        private void Apply(ActionItemAdded e)
        {
            ActionItems.Add(new ActionItem { Id = e.Id, Description = e.Description, ParticipantId = e.ParticipantId });
        }

        private void Apply(RetrospectiveStateChanged e)
        {
            State = e.TargetState;
        }

        private void Apply(LikeUpdated e)
        {
            Likes.First(l => l.Id == e.LikeIdentifier).Description = e.Description;
        }

        #endregion

        #region Private helpers
        private void CheckParticipant(string participantId)
        {
            if (!Participants.Contains(participantId))
            {
                throw new UninvitedParticipantException();
            }
        }

        private void CheckRetrospectiveInDesiredState(RetrospectiveState desiredState)
        {
            if (State != desiredState)
            {
                throw new RetrospectiveIsCollectingSuggestionsException();
            }
        }
        #endregion
    }
}
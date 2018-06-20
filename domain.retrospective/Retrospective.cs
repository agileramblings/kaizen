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
            CheckExistsAndCanModify(Likes, likeIdentifier, participantId);

            ApplyChange(new LikeUpdated(likeIdentifier, description));
        }
        public void DeleteLikeItem(Guid likeIdentifier, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectingSuggestions);
            CheckExistsAndCanModify(Likes, likeIdentifier, participantId);

            ApplyChange(new LikeDeleted(likeIdentifier));
        }

        public void AddDislikeItem(string description, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectingSuggestions);

            ApplyChange(new DislikeAdded(description, participantId));
        }
        public void UpdateDislikeItem(Guid dislikeIdentifier, string description, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectingSuggestions);
            CheckExistsAndCanModify(Dislikes, dislikeIdentifier, participantId);

            ApplyChange(new DislikeUpdated(dislikeIdentifier, description));
        }
        public void DeleteDislikeItem(Guid dislikeIdentifier, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectingSuggestions);
            CheckExistsAndCanModify(Dislikes, dislikeIdentifier, participantId);

            ApplyChange(new DislikeDeleted(dislikeIdentifier));
        }

        public void AddActionItem(string description, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectionActionItems);

            ApplyChange(new ActionItemAdded(description, participantId));
        }
        public void UpdateActionItem(Guid actionItemIdentifier, string description, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectionActionItems);
            CheckExistsAndCanModify(ActionItems, actionItemIdentifier, participantId);

            ApplyChange(new ActionItemUpdated(actionItemIdentifier, description));
        }
        public void DeleteActionItem(Guid actionItemIdentifier, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectionActionItems);
            CheckExistsAndCanModify(ActionItems, actionItemIdentifier, participantId);

            ApplyChange(new ActionItemDeleted(actionItemIdentifier));
        }
        public void ToggleLikeVote(Guid likeIdentifier, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectingSuggestions);
            CheckExists(Likes, likeIdentifier);

            ApplyChange(new LikeVoteToggled(likeIdentifier, participantId));
        }

        public void ToggleDislikeVote(Guid dislikeIdentifier, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectingSuggestions);
            CheckExists(Dislikes, dislikeIdentifier);

            ApplyChange(new DislikeVoteToggled(dislikeIdentifier, participantId));
        }

        public void ToggleActionItemVote(Guid aiIdentifier, string participantId)
        {
            CheckParticipant(participantId);
            CheckRetrospectiveInDesiredState(RetrospectiveState.CollectionActionItems);
            CheckExists(ActionItems, aiIdentifier);

            ApplyChange(new ActionItemVoteToggled(aiIdentifier, participantId));
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
        private void Apply(DislikeUpdated e)
        {
            Dislikes.First(dl => dl.Id == e.DislikeIdentifier).Description = e.Description;
        }
        private void Apply(ActionItemUpdated e)
        {
            ActionItems.First(dl => dl.Id == e.ActionItemIdentifier).Description = e.Description;
        }
        private void Apply(LikeDeleted e)
        {
            var like = Likes.First(l => l.Id == e.LikeIdentifier);
            Likes.Remove(like);
        }
        private void Apply(DislikeDeleted e)
        {
            var dislike = Dislikes.First(l => l.Id == e.DislikeIdentifier);
            Dislikes.Remove(dislike);
        }
        private void Apply(ActionItemDeleted e)
        {
            var actionItem = ActionItems.First(l => l.Id == e.ActionItemIdentifier);
            ActionItems.Remove(actionItem);
        }

        private void Apply(LikeVoteToggled e)
        {
            var likeItem = Likes.First(l => l.Id == e.LikeIdentifier);
            likeItem.ToggleVote(e.ParticipantId);
        }

        private void Apply(DislikeVoteToggled e)
        {
            var dislikeItem = Dislikes.First(l => l.Id == e.DislikeIdentifier);
            dislikeItem.ToggleVote(e.ParticipantId);
        }

        private void Apply(ActionItemVoteToggled e)
        {
            var actionItem = ActionItems.First(l => l.Id == e.ActionItemIdentifier);
            actionItem.ToggleVote(e.ParticipantId);
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
        private void CheckExistsAndCanModify<T>(IEnumerable<T> collection, Guid identifier, string participantId) where T : RetrospectiveItemBase
        {
            var item = collection.FirstOrDefault(l => l.Id == identifier);
            if (item == null)
            {
                throw new RetrospectiveItemDoesNotExistException(nameof(T), identifier);
            }

            if (item.ParticipantId != participantId)
            {
                throw new RetrospectiveItemNotOwnedByParticipantException(nameof(T), identifier, participantId);
            }
        }

        private void CheckExists<T>(IEnumerable<T> collection, Guid identifier) where T : RetrospectiveItemBase
        {
            var item = collection.FirstOrDefault(l => l.Id == identifier);
            if (item == null)
            {
                throw new RetrospectiveItemDoesNotExistException(nameof(T), identifier);
            }
        }

        #endregion


    }
}
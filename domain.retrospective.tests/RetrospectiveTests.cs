﻿using System;
using System.Linq;
using kaizen.domain.retrospective.events;
using kaizen.domain.retrospective.exceptions;
using Xunit;

namespace kaizen.domain.retrospective.tests
{
    public class RetrospectiveTests
    {
        private const string OwnerName = "dave.white@gettyimages.com";
        private readonly string[] _participants = {"tuan.nguyen@gettyimages.com", "david.chen@gettyimages.com" };
        private readonly Guid _defaultRetroId = Guid.Parse("12345678-1234-1234-1234-123456789012");

        [Fact]
        public void ARetrospectiveCanBeCreated()
        {
            // arrange

            // act
            var sut = GetDefaultRetrospectiveSut();

            // assert proper events created
            var events = sut.GetUncommittedChanges().ToList();
            Assert.Single(events);
            Assert.Equal(typeof(RetrospectiveCreated), events.First().GetType());
            Assert.Equal(OwnerName, ((RetrospectiveCreated)events.First()).Owner);
            Assert.NotEqual(default(DateTime), ((RetrospectiveCreated)events.First()).CreatedOn);

            // assert proper changes happened to aggregate
            Assert.Equal(_defaultRetroId, sut.Id);
            Assert.Equal(OwnerName, sut.Owner);
        }

        [Fact]
        public void AParticipantCanBeInvitedToARetrospective()
        {
            // arrange
            var sut = GetDefaultRetrospectiveSut();

            // act
            sut.InviteAParticipant(_participants[0]);

            // assert
            var events = sut.GetUncommittedChanges().ToList();
            Assert.Equal(2, events.Count);
            Assert.Equal(typeof(ParticipantAdded), events.Last().GetType());
            Assert.Single(sut.Participants);
            Assert.Equal(_participants[0], sut.Participants[0]);
        }

        [Fact]
        public void MultipleParticipantsCanBeInvitedToARetrospective()
        {
            // arrange
            var sut = GetDefaultRetrospectiveSut();

            // act
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);

            // assert
            var events = sut.GetUncommittedChanges().ToList();
            Assert.Equal(3, events.Count);
            Assert.Equal(typeof(ParticipantAdded), events.Last().GetType());
            Assert.Equal(2, sut.Participants.Count);
            Assert.Equal(_participants[0], sut.Participants[0]);
            Assert.Equal(_participants[1], sut.Participants[1]);
        }

        [Fact]
        public void AnInvitedParticipantCanAddANewLikeItem()
        {
            // arrange
            const string likeItemDescription = "a description of what we liked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);

            // act
            sut.AddLikeItem(likeItemDescription, _participants[0]);

            // assert
            var events = sut.GetUncommittedChanges().ToList();
            Assert.Equal(3, events.Count);
            Assert.Equal(typeof(LikeAdded), events.Last().GetType());

            Assert.Single(sut.Likes);
            Assert.Equal(likeItemDescription, sut.Likes.First().Description);
            Assert.Equal(_participants[0], sut.Likes.First().ParticipantId);
        }

        [Fact]
        public void AnUninvitedParticipantCannotAddANewLike()
        {
            // arrange
            const string likeItemDescription = "a description of what we liked";
            var sut = GetDefaultRetrospectiveSut();

            // act & assert
            Assert.Throws<UninvitedParticipantException>(() => sut.AddLikeItem(likeItemDescription, _participants[0]));

            // assert
            Assert.Empty(sut.Likes);
        }

        [Fact]
        public void AnInvitedParticipantCanAddANewDislike()
        {
            // arrange
            const string dislikeItemDescription = "a description of what we disliked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);

            // act
            sut.AddDislikeItem(dislikeItemDescription, _participants[0]);

            // assert
            var events = sut.GetUncommittedChanges().ToList();
            Assert.Equal(3, events.Count);
            Assert.Equal(typeof(DislikeAdded), events.Last().GetType());

            Assert.Single(sut.Dislikes);
            Assert.Equal(dislikeItemDescription, sut.Dislikes.First().Description);
            Assert.Equal(_participants[0], sut.Dislikes.First().ParticipantId);
        }

        [Fact]
        public void AnUninvitedParticipantCannotAddANewDislike()
        {
            // arrange
            const string dislikeItemDescription = "a description of what we disliked";
            var sut = GetDefaultRetrospectiveSut();

            // act & assert
            Assert.Throws<UninvitedParticipantException>(() => sut.AddDislikeItem(dislikeItemDescription, _participants[0]));

            // assert
            Assert.Empty(sut.Dislikes);
        }

        [Fact]
        public void AnInvitedParticipantCanAddAnActionItemIfRetrospectiveIsCollectingActionItems()
        {
            // arrange
            const string actionItemDescription = "a description of what we will try to do";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.StartCollectingActionItems(OwnerName);

            // act
            sut.AddActionItem(actionItemDescription, _participants[0]);

            // assert
            var events = sut.GetUncommittedChanges().ToList();
            Assert.Equal(4, events.Count);
            Assert.Equal(typeof(ActionItemAdded), events.Last().GetType());

            Assert.Single(sut.ActionItems);
            Assert.Equal(RetrospectiveState.CollectionActionItems, sut.State);
            Assert.Equal(actionItemDescription, sut.ActionItems.First().Description);
            Assert.Equal(_participants[0], sut.ActionItems.First().ParticipantId);
        }

        [Fact]
        public void AnUninvitedParticipantCannotAddAnActionItem()
        {
            // arrange
            const string actionItemDescription = "a description of what we will try to do";
            var sut = GetDefaultRetrospectiveSut();

            // act & assert
            Assert.Throws<UninvitedParticipantException>(() => sut.AddActionItem(actionItemDescription, _participants[0]));

            // assert
            Assert.Empty(sut.ActionItems);
        }

        [Fact]
        public void AnInvitedParticipantCannotAddAnActionItemWhileTheRetrospectiveIsCollectingSuggestions()
        {
            // arrange
            const string actionItemDescription = "a description of what we will try to do";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);

            // act & assert
            Assert.Throws<RetrospectiveIsCollectingSuggestionsException>(() => sut.AddActionItem(actionItemDescription, _participants[0]));

            // Assert
            Assert.Equal(RetrospectiveState.CollectingSuggestions, sut.State);
        }

        [Fact]
        public void AnOwnerCanChangeARetrospectiveToCollectingActionItemsState()
        {
            // arrange
            var sut = GetDefaultRetrospectiveSut();

            // act
            sut.StartCollectingActionItems(OwnerName);

            // assert
            Assert.Equal(RetrospectiveState.CollectionActionItems, sut.State);
        }

        [Fact]
        public void AnInvitedParticipantCannotChangeARetrospectiveToCollectingActionItemsState()
        {
            // arrange
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);

            // act
            Assert.Throws<ParticipantCannotChangeRetrospectiveStateException>(() => sut.StartCollectingActionItems(_participants[0]));

            // assert
            Assert.Equal(RetrospectiveState.CollectingSuggestions, sut.State);
        }

        [Fact]
        public void AnInvitedParticipantCanUpdateTheirOwnLike()
        {
            // arrange
            const string initialDescription = "Initial description";
            const string updateDescription = "Update to a description for my like";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.AddLikeItem(initialDescription, _participants[0]);
            Guid likeIdentifier = sut.Likes.First().Id;

            // act
            sut.UpdateLikeItem(likeIdentifier, updateDescription, _participants[0]);

            // assert
            Assert.Equal(updateDescription, sut.Likes.First(like => like.Id == likeIdentifier).Description);
        }

        [Fact]
        public void AnInvitedParticipantCannotUpdateSomeoneElsesLike()
        {
            // arrange
            const string initialDescription = "Initial description";
            const string updateDescription = "Update to a description for my like";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);

            sut.AddLikeItem(initialDescription, _participants[0]);
            Guid likeIdentifier = sut.Likes.First().Id;

            // act & assert
            Assert.Throws<RetrospectiveItemNotOwnedByParticipantException>(() => sut.UpdateLikeItem(likeIdentifier, updateDescription, _participants[1]));

            // Assert
            Assert.Equal(initialDescription, sut.Likes.First(l => l.Id == likeIdentifier).Description);
        }

        [Fact]
        public void AnInvitedParticipantCannotUpdateALikeThatDoesNotExist()
        {
            // arrange
            const string initialDescription = "Initial description";
            const string updateDescription = "Update to a description for my like";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.AddLikeItem(initialDescription, _participants[0]);
            Guid likeIdentifier = sut.Likes.First().Id;

            // act & assert
            Assert.Throws<RetrospectiveItemDoesNotExistException>(() => sut.UpdateLikeItem(Guid.Empty, updateDescription, _participants[0]));

            // Assert
            Assert.Equal(initialDescription, sut.Likes.First(l => l.Id == likeIdentifier).Description);
        }

        #region Test Helpers
        private Retrospective GetDefaultRetrospectiveSut() => new Retrospective(_defaultRetroId, OwnerName);
        #endregion
    }
}

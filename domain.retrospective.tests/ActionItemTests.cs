using System;
using System.Linq;
using kaizen.domain.retrospective.events;
using kaizen.domain.retrospective.exceptions;
using Xunit;

namespace kaizen.domain.retrospective.tests
{
    public class ActionItemTests : TestBase
    {
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
        public void AnInvitedParticipantCanUpdateTheirOwnAnActionItem()
        {
            // arrange
            const string actionItemDescription = "initial description";
            const string updateDescription = "Update to a description for my action item";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.StartCollectingActionItems(OwnerName);

            sut.AddActionItem(actionItemDescription, _participants[0]);
            Guid actionItemIdentifier = sut.ActionItems.First().Id;

            // act
            sut.UpdateActionItem(actionItemIdentifier, updateDescription, _participants[0]);

            // assert
            Assert.Equal(updateDescription, sut.ActionItems.First(ai => ai.Id == actionItemIdentifier).Description);
        }

        [Fact]
        public void AnInvitedParticipantCannotUpdateSomeoneElsesAnActionItem()
        {
            // arrange
            const string initialDescription = "Initial description";
            const string updateDescription = "Update to a description for my action item";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);
            sut.StartCollectingActionItems(OwnerName);

            sut.AddActionItem(initialDescription, _participants[0]);
            Guid actionItemIdentifier = sut.ActionItems.First().Id;

            // act & assert
            Assert.Throws<RetrospectiveItemNotOwnedByParticipantException>(() => sut.UpdateActionItem(actionItemIdentifier, updateDescription, _participants[1]));

            // Assert
            Assert.Equal(initialDescription, sut.ActionItems.First(ai => ai.Id == actionItemIdentifier).Description);
        }

        [Fact]
        public void AnInvitedParticipantCannotUpdateAnActionItemThatDoesNotExist()
        {
            // arrange
            const string initialDescription = "Initial description";
            const string updateDescription = "Update to a description for my action item";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.StartCollectingActionItems(OwnerName);

            sut.AddActionItem(initialDescription, _participants[0]);
            Guid actionItemIdentifier = sut.ActionItems.First().Id;

            // act & assert
            Assert.Throws<RetrospectiveItemDoesNotExistException>(() => sut.UpdateActionItem(Guid.Empty, updateDescription, _participants[0]));

            // Assert
            Assert.Equal(initialDescription, sut.ActionItems.First(ai => ai.Id == actionItemIdentifier).Description);
        }

        [Fact]
        public void AnInvitedParticipantCanDeleteTheirOwnActionItem()
        {
            // arrange
            const string initialDescription = "Initial description";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.StartCollectingActionItems(OwnerName);

            sut.AddActionItem(initialDescription, _participants[0]);
            Guid actionItemIdentifier = sut.ActionItems.First().Id;

            // act
            sut.DeleteActionItem(actionItemIdentifier, _participants[0]);

            // assert
            Assert.Empty(sut.ActionItems);
        }

        [Fact]
        public void AnInvitedParticipantCanDeleteSomeoneElsesActionItem()
        {
            // arrange
            const string initialDescription = "Initial description";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);
            sut.StartCollectingActionItems(OwnerName);

            sut.AddActionItem(initialDescription, _participants[0]);
            Guid actionItemIdentifier = sut.ActionItems.First().Id;

            // act
            Assert.Throws<RetrospectiveItemNotOwnedByParticipantException>(() => sut.DeleteActionItem(actionItemIdentifier, _participants[1]));

            // assert
            Assert.Single(sut.ActionItems);
        }
    }
}

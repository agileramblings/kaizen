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
    }
}

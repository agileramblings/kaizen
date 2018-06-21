using System;
using System.Linq;
using kaizen.domain.retrospective.events;
using kaizen.domain.retrospective.exceptions;
using Xunit;

namespace kaizen.domain.retrospective.tests
{
    public class CreateRetrospectiveTests : TestBase
    {
        [Fact]
        public void ARetrospectiveCanBeCreated()
        {
            // arrange

            // act
            var sut = GetDefaultRetrospectiveSut();

            // assert proper events created
            var events = sut.GetUncommittedChanges().ToList();
            Assert.Equal(2, events.Count);
            Assert.Equal(typeof(RetrospectiveCreated), events.First().GetType());
            Assert.Equal(OwnerName, ((RetrospectiveCreated)events.First()).Owner);
            Assert.Equal(typeof(ParticipantAdded), events.Last().GetType());
            Assert.Equal(OwnerName, ((ParticipantAdded)events.Last()).ParticipantIdentifier);
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
            Assert.Equal(3, events.Count);
            Assert.Equal(typeof(ParticipantAdded), events.Last().GetType());
            Assert.Equal(2, sut.Participants.Count);
            Assert.Equal(OwnerName, sut.Participants.First());
            Assert.Equal(_participants[0], sut.Participants.Last());
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
            Assert.Equal(4, events.Count);
            Assert.Equal(typeof(ParticipantAdded), events.Last().GetType());
            Assert.Equal(3, sut.Participants.Count);
            Assert.Equal(_participants[0], sut.Participants[1]);
            Assert.Equal(_participants[1], sut.Participants[2]);
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
    }
}

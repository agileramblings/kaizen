using System;
using System.Linq;
using kaizen.domain.retrospective.events;
using Xunit;

namespace kaizen.domain.retrospective.tests
{
    public class RetrospectiveTests
    {
        private const string OwnerName = "dave.white@gettyimages.com";
        private readonly string[] _participants = {"tuan.nguyen@gettyimages.com", "david.chen@gettyimages.com" };
        private readonly Guid _defaultRetroId = Guid.Parse("12345678-1234-1234-1234-123456789012");

        [Fact]
        public void WhenARetrospectiveIsCreated()
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
        public void WhenAParticipantIsInvitedToARetrospective()
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
        public void WhenMultipleParticipantsAreInvitedToARetrospective()
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

        #region Test Helpers
        private Retrospective GetDefaultRetrospectiveSut() => new Retrospective(_defaultRetroId, OwnerName);
        #endregion
    }
}

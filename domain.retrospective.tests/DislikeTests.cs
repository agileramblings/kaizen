using System;
using System.Linq;
using kaizen.domain.retrospective.events;
using kaizen.domain.retrospective.exceptions;
using Xunit;

namespace kaizen.domain.retrospective.tests
{
    public class DislikeTests : TestBase
    {
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
    }
}

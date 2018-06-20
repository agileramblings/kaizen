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

        [Fact]
        public void AnInvitedParticipantCanUpdateTheirOwnDislike()
        {
            // arrange
            const string initialDescription = "Initial description";
            const string updateDescription = "Update to a description for my Dislike";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.AddDislikeItem(initialDescription, _participants[0]);
            Guid dislikeIdentifier = sut.Dislikes.First().Id;

            // act
            sut.UpdateDislikeItem(dislikeIdentifier, updateDescription, _participants[0]);

            // assert
            Assert.Equal(updateDescription, sut.Dislikes.First(dl => dl.Id == dislikeIdentifier).Description);
        }

        [Fact]
        public void AnInvitedParticipantCannotUpdateSomeoneElsesDislike()
        {
            // arrange
            const string initialDescription = "Initial description";
            const string updateDescription = "Update to a description for my Dislike";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);

            sut.AddDislikeItem(initialDescription, _participants[0]);
            Guid dislikeIdentifier = sut.Dislikes.First().Id;

            // act & assert
            Assert.Throws<RetrospectiveItemNotOwnedByParticipantException>(() => sut.UpdateDislikeItem(dislikeIdentifier, updateDescription, _participants[1]));

            // Assert
            Assert.Equal(initialDescription, sut.Dislikes.First(dl => dl.Id == dislikeIdentifier).Description);
        }

        [Fact]
        public void AnInvitedParticipantCannotUpdateADislikeThatDoesNotExist()
        {
            // arrange
            const string initialDescription = "Initial description";
            const string updateDescription = "Update to a description for my Dislike";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.AddDislikeItem(initialDescription, _participants[0]);
            Guid dislikeIdentifier = sut.Dislikes.First().Id;

            // act & assert
            Assert.Throws<RetrospectiveItemDoesNotExistException>(() => sut.UpdateDislikeItem(Guid.Empty, updateDescription, _participants[0]));

            // Assert
            Assert.Equal(initialDescription, sut.Dislikes.First(dl => dl.Id == dislikeIdentifier).Description);
        }

        [Fact]
        public void AnInvitedParticipantCanDeleteTheirOwnDislike()
        {
            // arrange
            const string dislikeItemDescription = "a description of what we disliked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.AddDislikeItem(dislikeItemDescription, _participants[0]);
            Guid dislikeIdentifier = sut.Dislikes.First().Id;

            // act
            sut.DeleteDislikeItem(dislikeIdentifier, _participants[0]);

            // assert
            Assert.Empty(sut.Dislikes);
        }

        [Fact]
        public void AnInvitedParticipantCanDeleteSomeoneElsesDislike()
        {
            // arrange
            const string dislikeItemDescription = "a description of what we disliked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);

            sut.AddDislikeItem(dislikeItemDescription, _participants[0]);
            Guid dislikeIdentifier = sut.Dislikes.First().Id;

            // act
            Assert.Throws<RetrospectiveItemNotOwnedByParticipantException>(() => sut.DeleteDislikeItem(dislikeIdentifier, _participants[1]));

            // assert
            Assert.Single(sut.Dislikes);
        }
    }
}

using System;
using System.Linq;
using kaizen.domain.retrospective.events;
using kaizen.domain.retrospective.exceptions;
using Xunit;

namespace kaizen.domain.retrospective.tests
{
    public class LikeTests : TestBase
    {
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

        [Fact]
        public void AnInvitedParticipantCanDeleteTheirOwnLike()
        {
            // arrange
            const string initialDescription = "Initial description";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.AddLikeItem(initialDescription, _participants[0]);
            Guid likeIdentifier = sut.Likes.First().Id;

            // act
            sut.DeleteLikeItem(likeIdentifier, _participants[0]);

            // assert
            Assert.Empty(sut.Likes);
        }

        [Fact]
        public void AnInvitedParticipantCannotDeleteSomeoneElsesLike()
        {
            // arrange
            const string initialDescription = "Initial description";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);

            sut.AddLikeItem(initialDescription, _participants[0]);
            Guid likeIdentifier = sut.Likes.First().Id;

            // act & assert
            Assert.Throws<RetrospectiveItemNotOwnedByParticipantException>(() => sut.DeleteLikeItem(likeIdentifier, _participants[1]));

            // Assert
            Assert.Single(sut.Likes);
        }
    }
}

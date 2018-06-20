using System;
using System.Linq;
using Xunit;

namespace kaizen.domain.retrospective.tests
{
    public class VoteToggleTests : TestBase
    {
        #region Like Vote Tests

        [Fact]
        public void AnInvitedParticipantCanToggleOnTheirVoteOnALikeItem()
        {
            // arrange
            const string likeItemDescription = "a description of what we liked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.AddLikeItem(likeItemDescription, _participants[0]);
            Guid likeIdentifier = sut.Likes.First().Id;

            // act
            sut.ToggleLikeVote(likeIdentifier, _participants[0]);

            // assert
            Assert.Equal(1, sut.Likes.First(l => l.Id == likeIdentifier).Votes);
        }

        [Fact]
        public void AnInvitedParticipantCanToggleOffTheirVoteOnALikeItem()
        {
            // arrange
            const string likeItemDescription = "a description of what we liked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.AddLikeItem(likeItemDescription, _participants[0]);
            Guid likeIdentifier = sut.Likes.First().Id;
            sut.ToggleLikeVote(likeIdentifier, _participants[0]);

            // act
            sut.ToggleLikeVote(likeIdentifier, _participants[0]);

            // assert
            Assert.Equal(0, sut.Likes.First(l => l.Id == likeIdentifier).Votes);
        }

        [Fact]
        public void MultipleInvitedParticipantsCanToggleOnTheirVoteOnALikeItem()
        {
            // arrange
            const string likeItemDescription = "a description of what we liked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);
            sut.AddLikeItem(likeItemDescription, _participants[0]);
            Guid likeIdentifier = sut.Likes.First().Id;

            // act
            sut.ToggleLikeVote(likeIdentifier, _participants[0]);
            sut.ToggleLikeVote(likeIdentifier, _participants[1]);

            // assert
            Assert.Equal(2, sut.Likes.First(l => l.Id == likeIdentifier).Votes);
        }

        [Fact]
        public void MultipleInvitedParticipantsCanToggleOffTheirVoteOnALikeItem()
        {
            // arrange
            const string likeItemDescription = "a description of what we liked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);
            sut.AddLikeItem(likeItemDescription, _participants[0]);
            Guid likeIdentifier = sut.Likes.First().Id;
            sut.ToggleLikeVote(likeIdentifier, _participants[0]);
            sut.ToggleLikeVote(likeIdentifier, _participants[1]);

            // Assert initial starting condition
            Assert.Equal(2, sut.Likes.First(l => l.Id == likeIdentifier).Votes);

            // act and assert an increment
            sut.ToggleLikeVote(likeIdentifier, _participants[0]);
            Assert.Equal(1, sut.Likes.First(l => l.Id == likeIdentifier).Votes);

            // act and assert an increment
            sut.ToggleLikeVote(likeIdentifier, _participants[1]);
            Assert.Equal(0, sut.Likes.First(l => l.Id == likeIdentifier).Votes);

            // act and assert an increment
            sut.ToggleLikeVote(likeIdentifier, _participants[1]);
            Assert.Equal(1, sut.Likes.First(l => l.Id == likeIdentifier).Votes);
        }


        #endregion

        #region Dislike Vote Tests
        [Fact]
        public void AnInvitedParticipantCanToggleOnTheirVoteOnADislikeItem()
        {
            // arrange
            const string description = "a description of what we disliked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.AddDislikeItem(description, _participants[0]);
            Guid dislikeIdentifier = sut.Dislikes.First().Id;

            // act
            sut.ToggleDislikeVote(dislikeIdentifier, _participants[0]);

            // assert
            Assert.Equal(1, sut.Dislikes.First(l => l.Id == dislikeIdentifier).Votes);
        }

        [Fact]
        public void AnInvitedParticipantCanToggleOffTheirVoteOnADislikeItem()
        {
            // arrange
            const string description = "a description of what we disliked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.AddDislikeItem(description, _participants[0]);
            Guid dislikeIdentifier = sut.Dislikes.First().Id;
            sut.ToggleDislikeVote(dislikeIdentifier, _participants[0]);

            // act
            sut.ToggleDislikeVote(dislikeIdentifier, _participants[0]);

            // assert
            Assert.Equal(0, sut.Dislikes.First(l => l.Id == dislikeIdentifier).Votes);
        }

        [Fact]
        public void MultipleInvitedParticipantsCanToggleOnTheirVoteOnADislikeItem()
        {
            // arrange
            const string description = "a description of what we disliked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);
            sut.AddDislikeItem(description, _participants[0]);
            Guid dislikeIdentifier = sut.Dislikes.First().Id;

            // act
            sut.ToggleDislikeVote(dislikeIdentifier, _participants[0]);
            sut.ToggleDislikeVote(dislikeIdentifier, _participants[1]);

            // assert
            Assert.Equal(2, sut.Dislikes.First(l => l.Id == dislikeIdentifier).Votes);
        }

        [Fact]
        public void MultipleInvitedParticipantsCanToggleOffTheirVoteOnADislikeItem()
        {
            // arrange
            const string description = "a description of what we disliked";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);
            sut.AddDislikeItem(description, _participants[0]);
            Guid dislikeIdentifier = sut.Dislikes.First().Id;
            sut.ToggleDislikeVote(dislikeIdentifier, _participants[0]);
            sut.ToggleDislikeVote(dislikeIdentifier, _participants[1]);

            // Assert initial starting condition
            Assert.Equal(2, sut.Dislikes.First(l => l.Id == dislikeIdentifier).Votes);

            // act and assert an increment
            sut.ToggleDislikeVote(dislikeIdentifier, _participants[0]);
            Assert.Equal(1, sut.Dislikes.First(l => l.Id == dislikeIdentifier).Votes);

            // act and assert an increment
            sut.ToggleDislikeVote(dislikeIdentifier, _participants[1]);
            Assert.Equal(0, sut.Dislikes.First(l => l.Id == dislikeIdentifier).Votes);

            // act and assert an increment
            sut.ToggleDislikeVote(dislikeIdentifier, _participants[1]);
            Assert.Equal(1, sut.Dislikes.First(l => l.Id == dislikeIdentifier).Votes);
        }
        #endregion

        #region ActionItem Vote Tests
        [Fact]
        public void AnInvitedParticipantCanToggleOnTheirVoteOnAnActionItem()
        {
            // arrange
            const string description = "a description of what we want to action";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.StartCollectingActionItems(OwnerName);

            sut.AddActionItem(description, _participants[0]);
            Guid aiIdentifier = sut.ActionItems.First().Id;

            // act
            sut.ToggleActionItemVote(aiIdentifier, _participants[0]);

            // assert
            Assert.Equal(1, sut.ActionItems.First(l => l.Id == aiIdentifier).Votes);
        }

        [Fact]
        public void AnInvitedParticipantCanToggleOffTheirVoteOnAnActionItem()
        {
            // arrange
            const string description = "a description of what we want to action";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.StartCollectingActionItems(OwnerName);

            sut.AddActionItem(description, _participants[0]);
            Guid aiIdentifier = sut.ActionItems.First().Id;

            sut.ToggleActionItemVote(aiIdentifier, _participants[0]);

            // act
            sut.ToggleActionItemVote(aiIdentifier, _participants[0]);

            // assert
            Assert.Equal(0, sut.ActionItems.First(l => l.Id == aiIdentifier).Votes);
        }

        [Fact]
        public void MultipleInvitedParticipantsCanToggleOnTheirVoteOnAnActionItem()
        {
            // arrange
            const string description = "a description of what we want to action";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);
            sut.StartCollectingActionItems(OwnerName);

            sut.AddActionItem(description, _participants[0]);
            Guid aiIdentifier = sut.ActionItems.First().Id;

            // act
            sut.ToggleActionItemVote(aiIdentifier, _participants[0]);
            sut.ToggleActionItemVote(aiIdentifier, _participants[1]);

            // assert
            Assert.Equal(2, sut.ActionItems.First(l => l.Id == aiIdentifier).Votes);
        }

        [Fact]
        public void MultipleInvitedParticipantsCanToggleOffTheirVoteOnAnActionItem()
        {
            // arrange
            const string description = "a description of what we want to action";
            var sut = GetDefaultRetrospectiveSut();
            sut.InviteAParticipant(_participants[0]);
            sut.InviteAParticipant(_participants[1]);
            sut.StartCollectingActionItems(OwnerName);

            sut.AddActionItem(description, _participants[0]);
            Guid aiIdentifier = sut.ActionItems.First().Id;

            sut.ToggleActionItemVote(aiIdentifier, _participants[0]);
            sut.ToggleActionItemVote(aiIdentifier, _participants[1]);

            // Assert initial starting condition
            Assert.Equal(2, sut.ActionItems.First(l => l.Id == aiIdentifier).Votes);

            // act and assert an increment
            sut.ToggleActionItemVote(aiIdentifier, _participants[0]);
            Assert.Equal(1, sut.ActionItems.First(l => l.Id == aiIdentifier).Votes);

            // act and assert an increment
            sut.ToggleActionItemVote(aiIdentifier, _participants[1]);
            Assert.Equal(0, sut.ActionItems.First(l => l.Id == aiIdentifier).Votes);

            // act and assert an increment
            sut.ToggleActionItemVote(aiIdentifier, _participants[1]);
            Assert.Equal(1, sut.ActionItems.First(l => l.Id == aiIdentifier).Votes);
        }
        #endregion

        // Like object tests
        [Fact]
        public void AllRetrospectiveItemBaseObjectsCanBeVotedUp()
        {
            RetrospectiveItemBase sut1 = new Like();
            RetrospectiveItemBase sut2 = new Dislike();
            RetrospectiveItemBase sut3 = new ActionItem();

            Assert.Equal(0, sut1.Votes);
            Assert.Equal(0, sut2.Votes);
            Assert.Equal(0, sut3.Votes);

            sut1.ToggleVote("Some_user");
            sut2.ToggleVote("Some_user");
            sut3.ToggleVote("Some_user");

            Assert.Equal(1, sut1.Votes);
            Assert.Equal(1, sut2.Votes);
            Assert.Equal(1, sut3.Votes);
        }

        [Fact]
        public void AllRetrospectiveItemBaseObjectsCanBeVotedDown()
        {
            RetrospectiveItemBase sut1 = new Like();
            RetrospectiveItemBase sut2 = new Dislike();
            RetrospectiveItemBase sut3 = new ActionItem();

            sut1.ToggleVote("Some_user");
            sut2.ToggleVote("Some_user");
            sut3.ToggleVote("Some_user");

            Assert.Equal(1, sut1.Votes);
            Assert.Equal(1, sut2.Votes);
            Assert.Equal(1, sut3.Votes);

            sut1.ToggleVote("Some_user");
            sut2.ToggleVote("Some_user");
            sut3.ToggleVote("Some_user");

            Assert.Equal(0, sut1.Votes);
            Assert.Equal(0, sut2.Votes);
            Assert.Equal(0, sut3.Votes);
        }
    }
}

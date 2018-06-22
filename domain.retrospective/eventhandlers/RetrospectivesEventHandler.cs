using System.Linq;
using System.Threading.Tasks;
using kaizen.domain.@base;
using kaizen.domain.@base.persistence;
using kaizen.domain.retrospective.events;
using kaizen.domain.retrospective.readmodel;

namespace kaizen.domain.retrospective.eventhandlers
{
    public class RetrospectivesEventHandler : 
        IHandles<RetrospectiveCreated>,
        IHandles<ParticipantAdded>,
        IHandles<DislikeDeleted>,
        IHandles<LikeAdded>,
        IHandles<LikeUpdated>,
        IHandles<LikeDeleted>,
        IHandles<DislikeAdded>,
        IHandles<DislikeUpdated>,
        IHandles<ActionItemAdded>,
        IHandles<ActionItemUpdated>,
        IHandles<RetrospectiveStateChanged>,
        IHandles<LikeVoteToggled>,
        IHandles<DislikeVoteToggled>,
        IHandles<ActionItemVoteToggled>
    {
        private readonly IReadModelFacade _read;
        private readonly IReadModelPersistence _save;

        public RetrospectivesEventHandler(IReadModelFacade read, IReadModelPersistence save)
        {
            _read = read;
            _save = save;
        }

        public async Task Handle(RetrospectiveCreated message)
        {
            var newGame = new RetrospectiveDetails
            {
                Id = message.Id,
                Owner = message.Owner,
                CreatedOn = message.CreatedOn
            };
            await _save.Put(newGame);
        }

        public async Task Handle(ParticipantAdded message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            details.Participants.Add(message.ParticipantIdentifier);
            await _save.Put(details);
        }

        public async Task Handle(LikeAdded message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            details.Likes.Add(new Like {
                Id = message.LikeId,
                Description = message.Description
            });
            await _save.Put(details);
        }

        public async Task Handle(DislikeAdded message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            details.Dislikes.Add(new Dislike
            {
                Id = message.DislikeId,
                Description = message.Description
            });
            await _save.Put(details);
        }

        public async Task Handle(LikeUpdated message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            var like = details.Likes.FirstOrDefault(l => l.Id == message.LikeIdentifier);
            if (like != null)
            {
                like.Description = message.Description;
                await _save.Put(details);
            }
        }

        public async Task Handle(DislikeUpdated message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            var dislike = details.Dislikes.FirstOrDefault(l => l.Id == message.DislikeIdentifier);
            if (dislike != null)
            {
                dislike.Description = message.Description;
                await _save.Put(details);
            }
        }

        public async Task Handle(DislikeDeleted message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            details.Dislikes.Remove(details.Dislikes.FirstOrDefault(l => l.Id == message.DislikeIdentifier));
            await _save.Put(details);
        }

        public async Task Handle(LikeDeleted message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            details.Likes.Remove(details.Likes.FirstOrDefault(l => l.Id == message.LikeIdentifier));
            await _save.Put(details);
        }

        public async Task Handle(ActionItemAdded message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            details.ActionItems.Add(new ActionItem
            {
                Id = message.ActionItemId,
                Description = message.Description
            });
        }


        public async Task Handle(ActionItemUpdated message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            var actionItem = details.ActionItems.FirstOrDefault(l => l.Id == message.ActionItemIdentifier);
            if (actionItem != null)
            {
                actionItem.Description = message.Description;
                await _save.Put(details);
            }
        }

        public async Task Handle(RetrospectiveStateChanged message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            details.State = message.TargetState;
            await _save.Put(details);
        }

        public async Task Handle(LikeVoteToggled message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            var likeItem = details.Likes.First(c => c.Id == message.LikeIdentifier);
            likeItem.ToggleVote(message.ParticipantId);
            await _save.Put(details);
        }

        public async Task Handle(DislikeVoteToggled message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            var disLikeItem = details.Dislikes.First(c => c.Id == message.DislikeIdentifier);
            disLikeItem.ToggleVote(message.ParticipantId);
            await _save.Put(details);
        }

        public async Task Handle(ActionItemVoteToggled message)
        {
            var details = await _read.Get<RetrospectiveDetails>(message.RetrospectiveId);
            var actionItem = details.ActionItems.First(c => c.Id == message.ActionItemIdentifier);
            actionItem.ToggleVote(message.ParticipantId);
            await _save.Put(details);
        }
    }
}

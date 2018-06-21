using System.Threading.Tasks;
using kaizen.domain.@base.messaging;
using kaizen.domain.@base.persistence;
using kaizen.domain.retrospective.commands;

namespace kaizen.domain.retrospective.commandhandlers
{
    public class RetrospectivesCommandHandler :
        ICommandHandler<CreateRetrospective>,
        ICommandHandler<AddParticipant>,
        ICommandHandler<AddLike>,
        ICommandHandler<UpdateLike>,
        ICommandHandler<AddDislike>,
        ICommandHandler<AddActionItem>,
        ICommandHandler<UpdateRetroState>,
        ICommandHandler<Vote>
    {
        private readonly IAggregateRepository _store;

        public RetrospectivesCommandHandler(IAggregateRepository store)
        {
            _store = store;
        }

        public async Task Handle(AddActionItem command)
        {
            var retrospective = await _store.GetById<Retrospective>(command.RetrospectiveId);
            retrospective.AddLikeItem(command.Description, command.ParticipantId);
            await _store.Save(retrospective, retrospective.Version);
        }

        public async Task Handle(AddDislike command)
        {
            var retrospective = await _store.GetById<Retrospective>(command.RetrospectiveId);
            retrospective.AddLikeItem(command.Description, command.ParticipantId);
            await _store.Save(retrospective, retrospective.Version);
        }

        public async Task Handle(AddLike command)
        {
            var retrospective = await _store.GetById<Retrospective>(command.RetrospectiveId);
            retrospective.AddLikeItem(command.Description, command.ParticipantId);
            await _store.Save(retrospective, retrospective.Version);
        }
        public async Task Handle(UpdateLike command)
        {
            var retrospective = await _store.GetById<Retrospective>(command.RetrospectiveId);
            retrospective.UpdateLikeItem(command.LikeId, command.Description, command.ParticipantId);
            await _store.Save(retrospective, retrospective.Version);
        }

        public async Task Handle(AddParticipant command)
        {
            var retrospective = await _store.GetById<Retrospective>(command.RetrospectiveId);
            retrospective.InviteAParticipant(command.ParticipantId);
            await _store.Save(retrospective, retrospective.Version);
        }

        public async Task Handle(CreateRetrospective command)
        {
            var newRetrospective = new Retrospective(command.RetrospectiveId, command.Owner);
            await _store.Save(newRetrospective, -1);
        }

        public async Task Handle(UpdateRetroState command)
        {
            var retrospective = await _store.GetById<Retrospective>(command.RetrospectiveId);
            retrospective.UpdateRetroState(command.ParticipantId, command.State);
            await _store.Save(retrospective, retrospective.Version);
        }

        public async Task Handle(Vote command)
        {
            var retrospective = await _store.GetById<Retrospective>(command.RetrospectiveId);
            switch (command.ItemVoteType)
            {
                case Vote.VoteItemType.Like:
                    retrospective.ToggleLikeVote(command.ItemId, command.ParticipantId);
                    break;
                case Vote.VoteItemType.DisLike:
                    retrospective.ToggleDislikeVote(command.ItemId, command.ParticipantId);
                    break;
                case Vote.VoteItemType.ActionItem:
                    retrospective.ToggleActionItemVote(command.ItemId, command.ParticipantId);
                    break;
            }

            await _store.Save(retrospective, retrospective.Version);
        }
    }
}
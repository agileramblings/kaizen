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
        ICommandHandler<AddDislike>,
        ICommandHandler<AddActionItem>
    {
        private readonly IAggregateRepository _store;

        public RetrospectivesCommandHandler(IAggregateRepository store)
        {
            _store = store;
        }

        public async Task Handle(AddActionItem command)
        {
            var aggregateGame = await _store.GetById<Retrospective>(command.RetrospectiveId);
            aggregateGame.AddLikeItem(command.Description, command.ParticipantId);
            await _store.Save(aggregateGame, aggregateGame.Version);
        }

        public async Task Handle(AddDislike command)
        {
            var aggregateGame = await _store.GetById<Retrospective>(command.RetrospectiveId);
            aggregateGame.AddLikeItem(command.Description, command.ParticipantId);
            await _store.Save(aggregateGame, aggregateGame.Version);
        }

        public async Task Handle(AddLike command)
        {
            var aggregateGame = await _store.GetById<Retrospective>(command.RetrospectiveId);
            aggregateGame.AddLikeItem(command.Description, command.ParticipantId);
            await _store.Save(aggregateGame, aggregateGame.Version);
        }

        public async Task Handle(AddParticipant command)
        {
            var aggregateGame = await _store.GetById<Retrospective>(command.RetrospectiveId);
            aggregateGame.InviteAParticipant(command.ParticipantId);
            await _store.Save(aggregateGame, aggregateGame.Version);
        }

        public async Task Handle(CreateRetrospective command)
        {
            var newGame = new Retrospective(command.RetrospectiveId, command.Owner);
            await _store.Save(newGame, -1);
        }
    }
}
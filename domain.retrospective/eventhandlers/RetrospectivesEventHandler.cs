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
        IHandles<LikeAdded>,
        IHandles<LikeUpdated>
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
            details.Likes.Add(new Like {Id = message.LikeId,
                Description = message.Description,
                ParticipantId = message.ParticipantId});
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
    }
}

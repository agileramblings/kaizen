using System.Threading.Tasks;
using kaizen.domain.@base;
using kaizen.domain.@base.persistence;
using kaizen.domain.retrospective.events;
using kaizen.domain.retrospective.readmodel;

namespace kaizen.domain.retrospective.eventhandlers
{
    public class RetrospectivesEventHandler : 
        IHandles<RetrospectiveCreated>
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
    }
}

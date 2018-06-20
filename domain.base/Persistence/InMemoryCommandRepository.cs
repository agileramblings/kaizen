using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kaizen.domain.@base.Persistence
{
    public class InMemoryCommandRepository : ICommandRepository
    {
        private readonly List<CommandBase> _commands = new List<CommandBase>();

        public Task Save<T>(T command) where T : CommandBase
        {
            _commands.Add(command);
            return Task.FromResult(0);
        }

        public Task<IEnumerable<CommandBase>> Get(Guid tenantId, int skip, int take)
        {
            var retVal = _commands.OrderBy(c => c.ReceivedOn).Skip(skip).Take(take);
            return Task.FromResult(retVal);
        }
    }
}
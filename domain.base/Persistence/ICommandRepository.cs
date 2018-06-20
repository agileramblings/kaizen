using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kaizen.domain.@base.Persistence
{
    public interface ICommandRepository
    {
        Task Save<T>(T command) where T : CommandBase;
        Task<IEnumerable<CommandBase>> Get(Guid tenantId, int skip, int take);
    }
}

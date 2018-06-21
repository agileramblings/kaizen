using System.Collections.Generic;

namespace kaizen.domain.@base.messaging
{
    public interface ICommandHandlerFactory
    {
        IEnumerable<ICommandHandler<T>> GetHandlers<T>() where T : Command;
    }
}
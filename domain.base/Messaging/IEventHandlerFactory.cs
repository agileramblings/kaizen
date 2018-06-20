using System.Collections.Generic;

namespace kaizen.domain.@base.messaging
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IHandles<T>> GetHandlers<T>() where T : Event;
    }
}
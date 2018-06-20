using System.Threading.Tasks;

namespace kaizen.domain.@base
{
    public interface IEventPublisher
    {
        Task Publish<T>(T @event) where T : Event;
    }
}
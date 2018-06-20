using System.Threading.Tasks;

namespace kaizen.domain.@base
{
    public interface IHandles<T>
    {
        Task Handle(T message);
    }
}
using System.Threading.Tasks;

namespace kaizen.domain.@base.messaging
{
    public interface ICommandHandler<in T>
    {
        Task Handle(T command);
    }
}
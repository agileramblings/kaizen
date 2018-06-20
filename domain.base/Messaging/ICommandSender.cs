using System.Threading.Tasks;

namespace kaizen.domain.@base
{
    public interface ICommandSender
    {
        Task Send<T>(T command) where T : Command;
    }
}
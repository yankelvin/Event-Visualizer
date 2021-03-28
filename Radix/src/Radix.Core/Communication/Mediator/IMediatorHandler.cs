using Radix.Core.Messages;
using Radix.Core.Messages.Notifications;
using System.Threading.Tasks;

namespace Radix.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}

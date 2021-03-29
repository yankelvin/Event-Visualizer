using Radix.Events.Application.ViewModels;
using System.Threading.Tasks;

namespace Radix.WebApi.Hubs
{
    public interface IEventClient
    {
        Task ReceiveEvent(EventViewModel _event);
    }
}

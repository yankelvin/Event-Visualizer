using Microsoft.AspNetCore.SignalR;
using Radix.Events.Application.ViewModels;
using System.Threading.Tasks;

namespace Radix.Websocket
{
    public class EventHub : Hub<IEventClient>
    {
        public async Task SendEvent(EventViewModel _event)
        {
            await Clients.All.ReceiveEvent(_event);
        }
    }

    public interface IEventClient
    {
        Task ReceiveEvent(EventViewModel _event);
    }
}

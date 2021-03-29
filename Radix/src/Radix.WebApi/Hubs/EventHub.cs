using Microsoft.AspNetCore.SignalR;
using Radix.Events.Application.ViewModels;
using System.Threading.Tasks;

namespace Radix.WebApi.Hubs
{
    public class EventHub : Hub<IEventClient>
    {
        public async Task SendEvent(EventViewModel _event)
        {
            await Clients.All.ReceiveEvent(_event);
        }
    }
}

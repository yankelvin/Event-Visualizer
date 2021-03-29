using Radix.Core.Enums;
using Radix.Events.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Radix.Events.Application.Services
{
    public interface IEventAppService : IDisposable
    {
        Task<bool> InsertEvent(EventViewModel eventViewModel);
        Task<bool> UpdateEvent(EventViewModel eventViewModel);

        IEnumerable<EventViewModel> FindEvents();
        IEnumerable<EventViewModel> FindEventsByCountry(string country);
        IEnumerable<EventViewModel> FindEventsByRegion(Region region);
    }
}

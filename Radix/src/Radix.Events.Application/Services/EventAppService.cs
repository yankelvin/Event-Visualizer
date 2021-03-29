using AutoMapper;
using Radix.Core.Communication.Mediator;
using Radix.Core.Enums;
using Radix.Events.Application.ViewModels;
using Radix.Events.Domain.Commands;
using Radix.Events.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radix.Events.Application.Services
{
    public class EventAppService : IEventAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IEventRepository _eventRepository;

        public EventAppService(IMapper mapper, IMediatorHandler bus, IEventRepository eventRepository)
        {
            _bus = bus;
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<bool> InsertEvent(EventViewModel eventViewModel)
        {
            var command = _mapper.Map<InsertEventCommand>(eventViewModel);
            return await _bus.SendCommand(command);
        }

        public async Task<bool> UpdateEvent(EventViewModel eventViewModel)
        {
            eventViewModel.Status = string.IsNullOrEmpty(eventViewModel.Value) ? Status.Error : Status.Processed;

            var command = _mapper.Map<UpdateEventCommand>(eventViewModel);
            return await _bus.SendCommand(command);
        }

        public IEnumerable<EventViewModel> FindEvents()
        {
            var events = _eventRepository.Find(e => true).OrderByDescending(o => o.createdAt);

            return _mapper.Map<IEnumerable<EventViewModel>>(events);
        }

        public IEnumerable<EventViewModel> FindEventsByCountry(string country)
        {
            var events = _eventRepository.Find(e => e.Country.Equals(country));

            return _mapper.Map<IEnumerable<EventViewModel>>(events);
        }

        public IEnumerable<EventViewModel> FindEventsByRegion(Region region)
        {
            var events = _eventRepository.Find(e => e.Region == region);

            return _mapper.Map<IEnumerable<EventViewModel>>(events);
        }

        public void Dispose()
        {
            _eventRepository?.Dispose();
        }
    }
}

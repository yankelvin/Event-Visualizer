using AutoMapper;
using Radix.Events.Application.ViewModels;
using Radix.Events.Domain;
using Radix.Events.Domain.Commands;

namespace Radix.Events.Application.AutoMapper
{
    public class EventMapper : Profile
    {
        public EventMapper()
        {
            CreateMap<Event, EventViewModel>()
                .ForMember(e => e.Id, m => m.MapFrom(o => o.Id.ToString()));

            CreateMap<EventViewModel, InsertEventCommand>()
                .ConstructUsing(e => new InsertEventCommand(e.Id, e.Value, e.Country, e.Region, e.SensorName, e.Timestamp));

            CreateMap<EventViewModel, UpdateEventCommand>()
                .ConstructUsing(e => new UpdateEventCommand(e.Id, e.Value, e.Country, e.Region, e.SensorName, e.Timestamp));
        }
    }
}

using MediatR;
using MongoDB.Bson;
using Radix.Core.Communication.Mediator;
using Radix.Core.Enums;
using Radix.Core.Messages;
using Radix.Events.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Radix.Events.Domain.Commands
{
    public class EventCommandHandler : BaseCommandHandler,
                                       IRequestHandler<InsertEventCommand, bool>,
                                       IRequestHandler<UpdateEventCommand, bool>
    {
        private readonly IEventRepository _eventRepository;

        public EventCommandHandler(IMediatorHandler mediatorHandler,
            IEventRepository eventRepository) : base(mediatorHandler)
        {
            _eventRepository = eventRepository;
        }

        public async Task<bool> Handle(InsertEventCommand message, CancellationToken cancellationToken)
        {
            if (!ValidCommand(message))
                return false;

            var eventId = string.IsNullOrEmpty(message.Id) ? ObjectId.GenerateNewId() : new ObjectId(message.Id);

            var status = string.IsNullOrEmpty(message.Value) ? Status.Error : Status.Processed;

            var _event = new Event(eventId, message.Value, message.Country, message.Region, message.SensorName,
                message.TimeSpan, status);

            _eventRepository.InsertEvent(_event);

            return await _eventRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateEventCommand message, CancellationToken cancellationToken)
        {
            if (!ValidCommand(message))
                return false;

            var status = string.IsNullOrEmpty(message.Value) ? Status.Error : Status.Processed;

            var _event = new Event(new ObjectId(message.Id), message.Value, message.Country, message.Region, message.SensorName,
                message.TimeSpan, status);

            _eventRepository.UpdateEvent(_event);

            return await _eventRepository.UnitOfWork.Commit();
        }
    }
}

using AutoMapper;
using MongoDB.Bson;
using Moq;
using Moq.AutoMock;
using Radix.Core.Communication.Mediator;
using Radix.Core.Enums;
using Radix.Core.Messages;
using Radix.Events.Application.AutoMapper;
using Radix.Events.Application.Services;
using Radix.Events.Application.ViewModels;
using Radix.Events.Domain;
using Radix.Events.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Radix.Events.Application.Tests
{
    public class EventAppServiceFixture
    {
        public EventAppService EventAppService;
        public AutoMocker Mocker;

        public EventAppService GetEventAppService()
        {
            Mocker = new AutoMocker();

            Mocker.Use(new MapperConfiguration(ps =>
            {
                ps.AddProfile(new EventMapper());
            }).CreateMapper());

            EventAppService = Mocker.CreateInstance<EventAppService>();

            return EventAppService;
        }

        public EventViewModel GenerateEventViewModel(string id = null, string value = null, string country = null,
            Region region = Region.Norte, string sensorName = null, string timeStamp = null, Status status = Status.Processed)
        {
            var viewModel = new EventViewModel(id ?? ObjectId.GenerateNewId().ToString(), value ?? "value", country ?? "country",
                region, sensorName ?? "sensorName", timeStamp ?? "1617069959")
            {
                Status = status
            };

            return viewModel;
        }

        public Event GenerateEvent(ObjectId? id = null, string value = null, string country = null,
            Region region = Region.Norte, string sensorName = null, string timeStamp = null, Status status = Status.Processed)
        {
            return new Event(id ?? ObjectId.GenerateNewId(), value ?? "value", country ?? "country",
                region, sensorName ?? "sensorName", timeStamp ?? "1617069959", status);
        }

        public void SetupSendCommand<T>(bool result) where T : Command
        {
            Mocker.GetMock<IMediatorHandler>().Setup(m => m.SendCommand(It.IsAny<T>()))
                .Returns(Task.FromResult(result));
        }

        public void SetupFind(IEnumerable<Event> events)
        {
            Mocker.GetMock<IEventRepository>().Setup(e => e.Find(It.IsAny<Expression<Func<Event, bool>>>()))
                .Returns<Expression<Func<Event, bool>>>((expr) => events != null ? events.Where(expr.Compile()) : events);
        }
    }
}

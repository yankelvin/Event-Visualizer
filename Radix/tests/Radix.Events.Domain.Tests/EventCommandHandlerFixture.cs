using MongoDB.Bson;
using Moq.AutoMock;
using Radix.Core.Data;
using Radix.Core.Enums;
using Radix.Events.Domain.Commands;
using Radix.Events.Domain.Interfaces;
using System.Threading.Tasks;

namespace Radix.Events.Domain.Tests
{
    public class EventCommandHandlerFixture
    {
        public EventCommandHandler EventCommandHandler;
        public AutoMocker Mocker;

        public EventCommandHandler GetEventCommandHandler()
        {
            Mocker = new AutoMocker();

            EventCommandHandler = Mocker.CreateInstance<EventCommandHandler>();

            return EventCommandHandler;
        }

        public InsertEventCommand GenerateInsertEventCommand(string id = null, string value = null, string country = null,
            Region region = Region.Nordeste, string sensorName = null, string timeStamp = null)
        {
            return new InsertEventCommand(id ?? ObjectId.GenerateNewId().ToString(), value ?? "1000",
                country ?? "country", region, sensorName ?? "sensorName", timeStamp ?? "1617185140");
        }

        public UpdateEventCommand GenerateUpdateEventCommand(string id = null, string value = null, string country = null,
            Region region = Region.Nordeste, string sensorName = null, string timeStamp = null)
        {
            return new UpdateEventCommand(id ?? ObjectId.GenerateNewId().ToString(), value ?? "1000",
                country ?? "country", region, sensorName ?? "sensorName", timeStamp ?? "1617185140");
        }

        public void SetupCommit(bool result)
        {
            SetupUnitOfWork();

            Mocker.GetMock<IUnitOfWork>().Setup(m => m.Commit())
                .Returns(Task.FromResult(result));
        }

        public void SetupUnitOfWork()
        {
            Mocker.GetMock<IEventRepository>().Setup(e => e.UnitOfWork)
                .Returns(Mocker.GetMock<UnitOfWork>().Object);
        }
    }
}

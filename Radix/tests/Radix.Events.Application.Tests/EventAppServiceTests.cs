using AutoMapper;
using Moq;
using Radix.Core.Communication.Mediator;
using Radix.Core.Enums;
using Radix.Core.Messages;
using Radix.Events.Application.Services;
using Radix.Events.Domain.Commands;
using Radix.Events.Domain.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Radix.Events.Application.Tests
{
    public class EventAppServiceTests
    {
        public readonly EventAppServiceFixture _fixture;
        public readonly EventAppService _eventAppService;

        public EventAppServiceTests()
        {
            var mapper = new Mock<IMapper>();
            var mediator = new Mock<IMediatorHandler>();
            var eventRepository = new Mock<IEventRepository>();

            _eventAppService = new EventAppService(mapper.Object, mediator.Object, eventRepository.Object);
            _fixture = new EventAppServiceFixture();
        }

        [Fact(DisplayName = "Inserting a Event with Success")]
        public async Task InsertEvent_InsertEventWithSuccess_Success()
        {
            //Arrange
            var viewModel = _fixture.GenerateEventViewModel();

            //Setup
            new Mock<IMediatorHandler>().Setup(m => m.SendCommand(It.IsAny<Command>())).Returns(Task.FromResult(true));
            new Mock<IMapper>().Setup(m => m.Map<InsertEventCommand>(It.IsAny<object>())).Returns(new InsertEventCommand(null, null, null, Region.CentroOeste, null, null));

            //Act
            var result = await _eventAppService.InsertEvent(viewModel);

            //Assert
            Assert.True(result);
            new Mock<IMediatorHandler>().Verify(x => x.SendCommand(It.IsAny<InsertEventCommand>()), Times.Once);
        }
    }
}

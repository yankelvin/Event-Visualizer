using Moq;
using Radix.Core.Communication.Mediator;
using Radix.Events.Application.Services;
using Radix.Events.Domain;
using Radix.Events.Domain.Commands;
using Radix.Events.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            _fixture = new EventAppServiceFixture();
            _eventAppService = _fixture.GetEventAppService();
        }

        [Fact(DisplayName = "Inserting a Event with Success")]
        public async Task InsertEvent_InsertEventWithSuccess_Success()
        {
            // Arrange
            var viewModel = _fixture.GenerateEventViewModel();

            // Setup
            _fixture.SetupSendCommand<InsertEventCommand>(true);

            // Act
            var result = await _eventAppService.InsertEvent(viewModel);

            // Assert
            Assert.True(result);
            _fixture.Mocker.GetMock<IMediatorHandler>()
                .Verify(x => x.SendCommand(It.IsAny<InsertEventCommand>()), Times.Once);
        }

        [Fact(DisplayName = "Update a Event with Success")]
        public async Task UpdateEvent_UpdateEventWithSuccess_Success()
        {
            // Arrange
            var viewModel = _fixture.GenerateEventViewModel();

            // Setup
            _fixture.SetupSendCommand<UpdateEventCommand>(true);

            // Act
            var result = await _eventAppService.UpdateEvent(viewModel);

            // Assert
            Assert.True(result);
            _fixture.Mocker.GetMock<IMediatorHandler>()
                .Verify(x => x.SendCommand(It.IsAny<UpdateEventCommand>()), Times.Once);
        }

        [Fact(DisplayName = "Get All Events")]
        public void Find_GetAllEvents_Success()
        {
            // Arrange
            var events = new List<Event>() { _fixture.GenerateEvent(), _fixture.GenerateEvent() };

            // Setup
            _fixture.SetupFind(events);

            // Act
            var result = _eventAppService.FindEvents()?.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _fixture.Mocker.GetMock<IEventRepository>()
                .Verify(x => x.Find(It.IsAny<Expression<Func<Event, bool>>>()), Times.Once);
        }
    }
}

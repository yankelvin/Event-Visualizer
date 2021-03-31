using Moq;
using Radix.Core.Communication.Mediator;
using Radix.Core.Enums;
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

        [Fact(DisplayName = "Inserting a event with Success")]
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

        [Fact(DisplayName = "Update a event with Success")]
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

        [Fact(DisplayName = "Get all events with Sucess")]
        public void FindEvents_GetAllEvents_Success()
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
            Assert.True(events.Count == result.Count);
            _fixture.Mocker.GetMock<IEventRepository>()
                .Verify(x => x.Find(It.IsAny<Expression<Func<Event, bool>>>()), Times.Once);
        }

        [Fact(DisplayName = "Find events by country with Success")]
        public void FindEventsByCountry_FindAllEventsInACountry_Success()
        {
            // Arrange
            var country = "brasil";
            var events = new List<Event>() { _fixture.GenerateEvent(country: country), _fixture.GenerateEvent() };

            // Setup
            _fixture.SetupFind(events);

            // Act
            var result = _eventAppService.FindEventsByCountry(country)?.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
            _fixture.Mocker.GetMock<IEventRepository>()
                .Verify(x => x.Find(It.IsAny<Expression<Func<Event, bool>>>()), Times.Once);
        }

        [Fact(DisplayName = "Find events by region with Success")]
        public void FindEventsByRegion_FindAllEventsInARegion_Success()
        {
            // Arrange
            var region = Region.Nordeste;
            var events = new List<Event>() { _fixture.GenerateEvent(region: region), _fixture.GenerateEvent() };

            // Setup
            _fixture.SetupFind(events);

            // Act
            var result = _eventAppService.FindEventsByRegion(region)?.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
            _fixture.Mocker.GetMock<IEventRepository>()
                .Verify(x => x.Find(It.IsAny<Expression<Func<Event, bool>>>()), Times.Once);
        }
    }
}

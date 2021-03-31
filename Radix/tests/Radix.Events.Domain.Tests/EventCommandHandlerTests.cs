using Moq;
using Radix.Events.Domain.Commands;
using Radix.Events.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Radix.Events.Domain.Tests
{
    public class EventCommandHandlerTests
    {
        public readonly EventCommandHandlerFixture _fixture;
        public readonly EventCommandHandler _handler;

        public EventCommandHandlerTests()
        {
            _fixture = new EventCommandHandlerFixture();
            _handler = _fixture.GetEventCommandHandler();
        }

        [Fact(DisplayName = "Insert event with success")]
        public async Task InsertEventCommand_InsertEventWithSuccess_Success()
        {
            // Arrange
            var command = _fixture.GenerateInsertEventCommand();
            var cancellationToken = new CancellationToken();

            // Setup
            _fixture.SetupCommit(true);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _fixture.Mocker.GetMock<IEventRepository>()
                .Verify(x => x.InsertEvent(It.IsAny<Event>()), Times.Once);
        }

        [Fact(DisplayName = "Update event with success")]
        public async Task Update_UpdateEventWithSuccess_Success()
        {
            // Arrange
            var command = _fixture.GenerateUpdateEventCommand();
            var cancellationToken = new CancellationToken();

            // Setup
            _fixture.SetupCommit(true);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _fixture.Mocker.GetMock<IEventRepository>()
                .Verify(x => x.UpdateEvent(It.IsAny<Event>()), Times.Once);
        }
    }
}

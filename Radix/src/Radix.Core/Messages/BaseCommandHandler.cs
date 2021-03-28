using Radix.Core.Communication.Mediator;
using Radix.Core.Messages.Notifications;

namespace Radix.Core.Messages
{
    public abstract class BaseCommandHandler
    {
        private readonly IMediatorHandler _mediatorHandler;

        public BaseCommandHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected bool ValidCommand(Command message)
        {
            if (message.IsValid())
                return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}

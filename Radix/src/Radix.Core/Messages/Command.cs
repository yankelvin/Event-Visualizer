using FluentValidation.Results;
using MediatR;
using System;

namespace Radix.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime ControlDate { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public Command()
        {
            ControlDate = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

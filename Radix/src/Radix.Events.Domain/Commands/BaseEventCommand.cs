using FluentValidation;
using Radix.Core.Enums;
using Radix.Core.Messages;
using System;

namespace Radix.Events.Domain.Commands
{
    public class BaseEventCommand : Command
    {
        public string Id { get; private set; }
        public string Value { get; private set; }
        public string Country { get; private set; }
        public Region Region { get; private set; }
        public string SensorName { get; private set; }
        public TimeSpan TimeSpan { get; private set; }

        protected BaseEventCommand(string id, string value, string country, Region region,
            string sensorName, TimeSpan timeSpan)
        {
            Id = id;
            Value = value;
            Country = country;
            Region = region;
            SensorName = sensorName;
            TimeSpan = timeSpan;
        }

        public override bool IsValid()
        {
            ValidationResult = new BaseEventCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class BaseEventCommandValidation : AbstractValidator<BaseEventCommand>
    {
        public BaseEventCommandValidation()
        {
            RuleFor(c => c.Value)
                .NotEmpty()
                .WithMessage("Valor do evento não foi informado.");

            RuleFor(c => c.Country)
                .NotEmpty()
                .WithMessage("País do evento não foi informado.");

            RuleFor(c => c.SensorName)
                .NotEmpty()
                .WithMessage("Nome do sensor do evento não foi informado.");
        }
    }
}

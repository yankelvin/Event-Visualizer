using Radix.Core.Enums;
using System;

namespace Radix.Events.Domain.Commands
{
    public class UpdateEventCommand : BaseEventCommand
    {
        public UpdateEventCommand(string id, string value, string country, Region region,
            string sensorName, TimeSpan timeSpan)
            : base(id, value, country, region, sensorName, timeSpan)
        {
        }
    }
}

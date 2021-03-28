using Radix.Core.Enums;

namespace Radix.Events.Domain.Commands
{
    public class InsertEventCommand : BaseEventCommand
    {
        public InsertEventCommand(string id, string value, string country, Region region,
            string sensorName, string timeStamp)
            : base(id, value, country, region, sensorName, timeStamp)
        {
        }
    }
}

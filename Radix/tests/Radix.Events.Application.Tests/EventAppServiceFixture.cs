using MongoDB.Bson;
using Radix.Core.Enums;
using Radix.Events.Application.ViewModels;

namespace Radix.Events.Application.Tests
{
    public class EventAppServiceFixture
    {
        public EventViewModel GenerateEventViewModel(string id = null, string value = null, string country = null,
            Region region = Region.Norte, string sensorName = null, string timeStamp = null, Status status = Status.Processed)
        {
            var viewModel = new EventViewModel(id ?? ObjectId.GenerateNewId().ToString(), value ?? "value", country ?? "country",
                region, sensorName ?? "sensorName", timeStamp ?? "1617069959")
            {
                Status = status
            };

            return viewModel;
        }
    }
}

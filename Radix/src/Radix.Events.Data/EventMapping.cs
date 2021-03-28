using MongoDB.Bson.Serialization;
using Radix.Events.Domain;

namespace Radix.Events.Data
{
    public class EventMapping
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Event>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}

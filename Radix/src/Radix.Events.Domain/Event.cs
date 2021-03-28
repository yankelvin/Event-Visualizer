using MongoDB.Bson;
using Radix.Core.DomainObjects;
using Radix.Core.Enums;
using System;

namespace Radix.Events.Domain
{
    public class Event : Document
    {
        public string Value { get; private set; }
        public string Country { get; private set; }
        public Region Region { get; private set; }
        public string SensorName { get; private set; }
        public TimeSpan Timestamp { get; private set; }
        public Status Status { get; private set; }

        public Event(ObjectId id, string value, string country, Region region,
            string sensorName, TimeSpan timestamp, Status status)
        {
            Id = id;
            Value = value;
            Country = country;
            Region = region;
            SensorName = sensorName;
            Timestamp = timestamp;
            Status = status;

            Validate();
        }

        public override void Validate()
        {
            Validations.ValidIfEmpty(Value, "Valor do evento não foi informado.");
            Validations.ValidIfEmpty(Country, "País do evento não foi informado.");
            Validations.ValidIfEmpty(SensorName, "Nome do sensor do evento não foi informado.");
        }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Radix.Core.Data
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        DateTime createdAt { get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        DateTime updatedAt { get; }
    }
}

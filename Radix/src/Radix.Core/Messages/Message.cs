using MongoDB.Bson;

namespace Radix.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public ObjectId ObjectId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}

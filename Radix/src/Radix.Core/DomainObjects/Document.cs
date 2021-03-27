using MongoDB.Bson;
using Radix.Core.Data;
using System;

namespace Radix.Core.DomainObjects
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        protected Document()
        {
            Id = ObjectId.GenerateNewId();
            createdAt = DateTime.Now;
            updatedAt = DateTime.Now;
        }

        public abstract void Validate();

        public override bool Equals(object obj)
        {
            var compareTo = obj as Document;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (ReferenceEquals(null, compareTo))
                return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Document a, Document b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return true;

            return a.Equals(b);
        }

        public static bool operator !=(Document a, Document b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}

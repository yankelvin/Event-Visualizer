using MongoDB.Driver;
using Radix.Core.Attributes;
using Radix.Core.Data;
using Radix.Core.DomainObjects;
using Radix.Events.Domain;
using Radix.Events.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Radix.Events.Data
{
    [BsonCollection("Events")]
    public class EventRepository : BaseRepository, IEventRepository
    {
        public IMongoCollection<Event> _collection { get; }

        public EventRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            var dataBase = UnitOfWork._context.GetDatabase();
            _collection = dataBase.GetCollection<Event>(GetCollectionName(typeof(EventRepository)));
        }

        public IEnumerable<Event> Find(Expression<Func<Event, bool>> filter)
        {
            return _collection.Find(filter).ToEnumerable();
        }

        public void InsertEvent(Event _event)
        {
            UnitOfWork._context.AddCommand(() => _collection.InsertOneAsync(_event));
        }

        public void UpdateEvent(Event _event)
        {
            var filter = Builders<Event>.Filter.Eq(doc => doc.Id, _event.Id);
            UnitOfWork._context.AddCommand(() => _collection.FindOneAndReplaceAsync(filter, _event));
        }
    }
}

using Radix.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Radix.Events.Domain.Interfaces
{
    public interface IEventRepository : IRepository
    {
        IEnumerable<Event> Find(Expression<Func<Event, bool>> filter);
        void InsertEvent(Event _event);
        void UpdateEvent(Event _event);
    }
}

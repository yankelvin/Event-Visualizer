using Radix.Core.Attributes;
using Radix.Core.Data;
using System;
using System.Linq;

namespace Radix.Core.DomainObjects
{
    public abstract class BaseRepository : IRepository
    {
        public IUnitOfWork UnitOfWork { get; private set; }

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}

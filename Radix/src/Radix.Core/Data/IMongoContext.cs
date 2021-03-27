using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Radix.Core.Data
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoDatabase GetDatabase();
    }
}

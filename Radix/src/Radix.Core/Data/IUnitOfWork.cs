using System;
using System.Threading.Tasks;

namespace Radix.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IMongoContext _context { get; }
        Task<bool> Commit();
    }
}

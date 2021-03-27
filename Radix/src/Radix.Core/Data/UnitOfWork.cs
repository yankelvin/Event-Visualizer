using System;
using System.Threading.Tasks;

namespace Radix.Core.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMongoContext _context { get; }

        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

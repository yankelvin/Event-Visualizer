using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radix.Core.Data
{
    public class MongoContext : IMongoContext
    {
        private IMongoDbSettings _settings { get; }
        private static IMongoDatabase Database { get; set; }
        private IClientSessionHandle Session { get; set; }
        private MongoClient MongoClient { get; set; }
        private List<Func<Task>> _commands;

        public MongoContext(IMongoDbSettings settings)
        {
            _settings = settings;
            _commands = new List<Func<Task>>();
            MongoClient = new MongoClient(_settings.ConnectionString);
        }

        public IMongoDatabase GetDatabase()
        {
            if (Database == null)
                Database = MongoClient.GetDatabase(_settings.DatabaseName);

            return Database;
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public async Task<int> SaveChanges()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();

                var count = _commands.Count;
                _commands = new List<Func<Task>>();

                return count;
            }
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

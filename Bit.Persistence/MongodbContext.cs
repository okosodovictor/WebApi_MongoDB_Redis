using Bit.Domain.Entities;
using MongoDB.Driver;

namespace Bit.Persistence
{
    public class MongodbContext
    {
        // for production code connection string should be retrieved from web config.

        private readonly string connectionString = "mongodb://localhost:27017";
        private readonly string databaseName = "PeopleData";

        private  readonly IMongoDatabase _database;
        private readonly IMongoClient client;

        public MongodbContext()
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            client = new MongoClient(settings);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Person> personCollection => _database.GetCollection<Person>("People");

        public IMongoCollection<User> UserCollection => _database.GetCollection<User>("User");
    }
}

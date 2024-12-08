using Augury.RepairTelemetryService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Augury.RepairTelemetryService.Repository
{
    public class BaseRepository: IBaseRepository
    {
        private readonly IMongoDatabase database;
        public BaseRepository(IOptions<DatabaseSettings> options, IConfiguration configuration) {
            string connstring = configuration.GetConnectionString("MongoDb");
            if(!string.IsNullOrWhiteSpace(connstring))
            {
                var client = new MongoClient(connstring);
                database = client.GetDatabase(options.Value.DatabaseName);
            }            
        }

        public IMongoDatabase Database
        {
            get { return database; }
        }
    }
}

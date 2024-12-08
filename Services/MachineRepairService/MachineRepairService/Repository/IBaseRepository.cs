using MongoDB.Driver;

namespace Augury.RepairTelemetryService.Repository
{
    public interface IBaseRepository
    {
        IMongoDatabase Database { get; }
    }
}

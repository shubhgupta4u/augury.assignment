using MongoDB.Driver;

namespace Augury.MachineInfoService.Repository
{
    public interface IBaseRepository
    {
        IMongoDatabase Database { get; }
    }
}

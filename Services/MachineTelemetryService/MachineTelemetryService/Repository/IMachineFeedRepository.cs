using Augury.RepairTelemetryService.Models.Entity;

namespace Augury.RepairTelemetryService.Repository
{
    public interface IMachineFeedRepository
    {
        Task InsertMachineFeedAsync(MachineFeed machineFeed);
        Task<MachineFeed> GetMachineFeedByIdAsync(string id);
        Task<MachineFeed[]> GetMachineFeedByMachineIdAsync(string id);
        Task<List<MachineFeed>> GetAllMachineFeedsAsync();
        Task UpdateMachineFeedAsync(string id, MachineFeed updatedMachineFeed);
        Task DeleteMachineFeedAsync(string id);
    }
}

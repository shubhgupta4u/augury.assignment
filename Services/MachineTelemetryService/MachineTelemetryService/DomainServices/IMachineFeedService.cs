using Augury.RepairTelemetryService.Models.Domain;

namespace Augury.RepairTelemetryService.DomainServices
{
    public interface IMachineFeedService
    {
        Task<ReadMachineFeedModel[]> GetMachineFeedsAsync();
        Task<ReadMachineFeedModel[]> GetMachineFeedByMachineIdAsync(string id);
        Task<ReadMachineFeedModel> GetMachineFeedByIdAsync(string id);
        Task<ReadMachineFeedModel[]> GetMachineFeedByIdAsync(string[] ids);
    }
}

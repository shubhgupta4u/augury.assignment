using Augury.RepairTelemetryService.Models.Domain;

namespace Augury.RepairTelemetryService.DomainServices
{
    public interface IRepairLogService
    {
        Task<ReadRepairLogModel[]> GetRepairLogsAsync();
        Task<ReadRepairLogModel> GetRepairLogByIdAsync(string id);
        Task<ReadRepairLogModel[]> GetRepairLogByIdAsync(string[] ids);
        Task<ReadRepairLogModel[]> GetRepairLogsByMachineIdAsync(string id);
    }
}

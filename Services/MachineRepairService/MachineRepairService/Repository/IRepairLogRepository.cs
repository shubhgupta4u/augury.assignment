using Augury.RepairTelemetryService.Models.Domain;
using Augury.RepairTelemetryService.Models.Entity;

namespace Augury.RepairTelemetryService.Repository
{
    public interface IRepairLogRepository
    {
        Task InsertRepairLogAsync(RepairLog RepairLog);
        Task<RepairLog> GetRepairLogByIdAsync(string id);
        Task<RepairLog[]> GetRepairLogsByMachineIdAsync(string id);
        Task<List<RepairLog>> GetAllRepairLogsAsync();
        Task UpdateRepairLogAsync(string id, RepairLog updatedRepairLog);
        Task DeleteRepairLogAsync(string id);
    }
}

namespace Augury.RepairTelemetryService.Repository
{
    using Augury.RepairTelemetryService.Models.Entity;
    using MongoDB.Driver;

    public class RepairLogRepository: IRepairLogRepository
    {
        private readonly IMongoCollection<RepairLog> _repairLogs;

        public RepairLogRepository(IBaseRepository baseRepository) 
        {
            // Assume the collection name is "repairLogs"
            _repairLogs = baseRepository.Database.GetCollection<RepairLog>("repairlogs");
        }

        // Insert a new RepairLog
        public async Task InsertRepairLogAsync(RepairLog repairLog)
        {
            if (repairLog == null) throw new ArgumentNullException(nameof(repairLog));
            await _repairLogs.InsertOneAsync(repairLog);
        }

        // Get a RepairLog by ID
        public async Task<RepairLog> GetRepairLogByIdAsync(string id)
        {
            return await _repairLogs.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        // Get a RepairLog by MachineId
        public async Task<RepairLog[]> GetRepairLogsByMachineIdAsync(string id)
        {
            var repairLogs = await _repairLogs.Find(m => m.Machine.Id == id).ToListAsync();
            return repairLogs.ToArray();
        }

        // Get all RepairLogs
        public async Task<List<RepairLog>> GetAllRepairLogsAsync()
        {
            return await _repairLogs.Find(_ => true).ToListAsync();
        }

        // Update a RepairLog by ID
        public async Task UpdateRepairLogAsync(string id, RepairLog updatedRepairLog)
        {
            if (updatedRepairLog == null) throw new ArgumentNullException(nameof(updatedRepairLog));
            await _repairLogs.ReplaceOneAsync(m => m.Id == id, updatedRepairLog);
        }

        // Delete a RepairLog by ID
        public async Task DeleteRepairLogAsync(string id)
        {
            await _repairLogs.DeleteOneAsync(m => m.Id == id);
        }
    }

}

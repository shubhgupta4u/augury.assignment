namespace Augury.RepairTelemetryService.Repository
{
    using Augury.RepairTelemetryService.Models.Entity;
    using MongoDB.Driver;

    public class MachineFeedRepository: IMachineFeedRepository
    {
        private readonly IMongoCollection<MachineFeed> _machineFeeds;

        public MachineFeedRepository(IBaseRepository baseRepository) 
        {
            // Assume the collection name is "machineFeeds"
            _machineFeeds = baseRepository.Database.GetCollection<MachineFeed>("feeds");
        }

        // Insert a new MachineFeed
        public async Task InsertMachineFeedAsync(MachineFeed machineFeed)
        {
            if (machineFeed == null) throw new ArgumentNullException(nameof(machineFeed));
            await _machineFeeds.InsertOneAsync(machineFeed);
        }

        // Get a MachineFeed by ID
        public async Task<MachineFeed> GetMachineFeedByIdAsync(string id)
        {
            return await _machineFeeds.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        // Get a MachineFeed by MachineId
        public async Task<MachineFeed[]> GetMachineFeedByMachineIdAsync(string id)
        {
            var feeds = await _machineFeeds.Find(m => m.MachineId == id).ToListAsync();
            return feeds.ToArray();
        }

        // Get all MachineFeeds
        public async Task<List<MachineFeed>> GetAllMachineFeedsAsync()
        {
            return await _machineFeeds.Find(_ => true).ToListAsync();
        }

        // Update a MachineFeed by ID
        public async Task UpdateMachineFeedAsync(string id, MachineFeed updatedMachineFeed)
        {
            if (updatedMachineFeed == null) throw new ArgumentNullException(nameof(updatedMachineFeed));
            await _machineFeeds.ReplaceOneAsync(m => m.Id == id, updatedMachineFeed);
        }

        // Delete a MachineFeed by ID
        public async Task DeleteMachineFeedAsync(string id)
        {
            await _machineFeeds.DeleteOneAsync(m => m.Id == id);
        }
    }

}

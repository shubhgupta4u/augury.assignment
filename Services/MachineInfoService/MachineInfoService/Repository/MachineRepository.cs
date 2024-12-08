namespace Augury.MachineInfoService.Repository
{
    using Augury.MachineInfoService.Models.Entity;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MachineRepository: IMachineRepository
    {
        private readonly IMongoCollection<Machine> _machines;

        public MachineRepository(IBaseRepository baseRepository) 
        {
            // Assume the collection name is "machines"
            _machines = baseRepository.Database.GetCollection<Machine>("machines");
        }

        // Insert a new Machine
        public async Task InsertMachineAsync(Machine machine)
        {
            if (machine == null) throw new ArgumentNullException(nameof(machine));
            await _machines.InsertOneAsync(machine);
        }

        // Get a Machine by ID
        public async Task<Machine> GetMachineByIdAsync(string id)
        {
            return await _machines.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        // Get all Machines
        public async Task<List<Machine>> GetAllMachinesAsync()
        {
            return await _machines.Find(_ => true).ToListAsync();
        }

        // Update a Machine by ID
        public async Task UpdateMachineAsync(string id, Machine updatedMachine)
        {
            if (updatedMachine == null) throw new ArgumentNullException(nameof(updatedMachine));
            await _machines.ReplaceOneAsync(m => m.Id == id, updatedMachine);
        }

        // Delete a Machine by ID
        public async Task DeleteMachineAsync(string id)
        {
            await _machines.DeleteOneAsync(m => m.Id == id);
        }
    }

}

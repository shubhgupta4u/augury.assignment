using Augury.MachineInfoService.Models.Entity;

namespace Augury.MachineInfoService.Repository
{
    public interface IMachineRepository
    {
        Task InsertMachineAsync(Machine machine);
        Task<Machine> GetMachineByIdAsync(string id);
        Task<List<Machine>> GetAllMachinesAsync();
        Task UpdateMachineAsync(string id, Machine updatedMachine);
        Task DeleteMachineAsync(string id);
    }
}

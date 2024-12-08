using Augury.MachineInfoService.Models.Domain;

namespace Augury.MachineInfoService.DomainServices
{
    public interface IMachineService
    {
        Task<ReadMachineModel[]> GetMachinesAsync();
        Task<ReadMachineModel> GetMachineByIdAsync(string id);
        Task<ReadMachineModel[]> GetMachineByIdAsync(string[] ids);
    }
}

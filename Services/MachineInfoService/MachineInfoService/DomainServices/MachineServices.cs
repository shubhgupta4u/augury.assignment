using Augury.MachineInfoService.Models.Domain;
using Augury.MachineInfoService.Repository;
using AutoMapper;

namespace Augury.MachineInfoService.DomainServices
{
    public class MachineServices: IMachineService
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;

        public MachineServices(IMachineRepository machineRepository, IMapper mapper)
        {
            _machineRepository = machineRepository;
            _mapper = mapper;
        }

        public async Task<ReadMachineModel[]> GetMachinesAsync()
        {
            var machine = await _machineRepository.GetAllMachinesAsync();
            return _mapper.Map<List<ReadMachineModel>>(machine).ToArray();
        }

        public async Task<ReadMachineModel> GetMachineByIdAsync(string id)
        {
            var machine = await _machineRepository.GetMachineByIdAsync(id);
            return _mapper.Map<ReadMachineModel>(machine);
        }

        public async Task<ReadMachineModel[]> GetMachineByIdAsync(string[] ids)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            List<ReadMachineModel> readMachineModels = new List<ReadMachineModel>();
            await Parallel.ForEachAsync<string>(ids, async(id, cancellationToken) =>
            {
                readMachineModels.Add(await GetMachineByIdAsync(id));
            });

            return readMachineModels.ToArray();
        }
    }
}

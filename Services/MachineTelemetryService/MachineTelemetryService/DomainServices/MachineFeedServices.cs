using Augury.RepairTelemetryService.Models.Domain;
using Augury.RepairTelemetryService.Repository;
using AutoMapper;

namespace Augury.RepairTelemetryService.DomainServices
{
    public class MachineFeedServices: IMachineFeedService
    {
        private readonly IMachineFeedRepository _machineFeedRepository;
        private readonly IMapper _mapper;

        public MachineFeedServices(IMachineFeedRepository machineFeedRepository, IMapper mapper)
        {
            _machineFeedRepository = machineFeedRepository;
            _mapper = mapper;
        }

        public async Task<ReadMachineFeedModel[]> GetMachineFeedsAsync()
        {
            var machineFeed = await _machineFeedRepository.GetAllMachineFeedsAsync();
            return _mapper.Map<List<ReadMachineFeedModel>>(machineFeed).ToArray();
        }
        public async Task<ReadMachineFeedModel[]> GetMachineFeedByMachineIdAsync(string id)
        {
            var feeds = await _machineFeedRepository.GetMachineFeedByMachineIdAsync(id);
            return _mapper.Map<List<ReadMachineFeedModel>>(feeds).ToArray();
        }
        public async Task<ReadMachineFeedModel> GetMachineFeedByIdAsync(string id)
        {
            var machineFeed = await _machineFeedRepository.GetMachineFeedByIdAsync(id);
            return _mapper.Map<ReadMachineFeedModel>(machineFeed);
        }

        public async Task<ReadMachineFeedModel[]> GetMachineFeedByIdAsync(string[] ids)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            List<ReadMachineFeedModel> readMachineFeedModels = new List<ReadMachineFeedModel>();
            await Parallel.ForEachAsync<string>(ids, async(id, cancellationToken) =>
            {
                readMachineFeedModels.Add(await GetMachineFeedByIdAsync(id));
            });

            return readMachineFeedModels.ToArray();
        }
    }
}

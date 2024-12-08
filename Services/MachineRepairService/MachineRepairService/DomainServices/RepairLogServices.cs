using Augury.RepairTelemetryService.Models.Domain;
using Augury.RepairTelemetryService.Models.Entity;
using Augury.RepairTelemetryService.Repository;
using AutoMapper;

namespace Augury.RepairTelemetryService.DomainServices
{
    public class RepairLogServices: IRepairLogService
    {
        private readonly IRepairLogRepository _repairLogRepository;
        private readonly IMapper _mapper;

        public RepairLogServices(IRepairLogRepository repairLogRepository, IMapper mapper)
        {
            _repairLogRepository = repairLogRepository;
            _mapper = mapper;
        }

        public async Task<ReadRepairLogModel[]> GetRepairLogsAsync()
        {
            var repairLog = await _repairLogRepository.GetAllRepairLogsAsync();
            return _mapper.Map<List<ReadRepairLogModel>>(repairLog).ToArray();
        }

        public async Task<ReadRepairLogModel> GetRepairLogByIdAsync(string id)
        {
            var repairLog = await _repairLogRepository.GetRepairLogByIdAsync(id);
            return _mapper.Map<ReadRepairLogModel>(repairLog);
        }

        public async Task<ReadRepairLogModel[]> GetRepairLogByIdAsync(string[] ids)
        {
            CancellationToken cancellationToken = CancellationToken.None;
            List<ReadRepairLogModel> readRepairLogModels = new List<ReadRepairLogModel>();
            await Parallel.ForEachAsync<string>(ids, async(id, cancellationToken) =>
            {
                readRepairLogModels.Add(await GetRepairLogByIdAsync(id));
            });

            return readRepairLogModels.ToArray();
        }

        public async Task<ReadRepairLogModel[]> GetRepairLogsByMachineIdAsync(string id)
        {
            var repairLogs = await _repairLogRepository.GetRepairLogsByMachineIdAsync(id);
            return _mapper.Map<List<ReadRepairLogModel>>(repairLogs).ToArray();
        }
    }
}

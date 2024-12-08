using Augury.RepairTelemetryService.DomainServices;
using Augury.RepairTelemetryService.Models.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Augury.RepairTelemetryService.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RepairLogController : ControllerBase
    {
        private readonly IRepairLogService _repairLogService;

        public RepairLogController(IRepairLogService repairLogService) {
            _repairLogService = repairLogService;
        }
        [HttpGet()]
        public async Task<ActionResult<ReadRepairLogModel[]>> GetAll()
        {
            var repairLogs = await _repairLogService.GetRepairLogsAsync();
            return Ok(repairLogs);
        }

        [HttpGet("machine/{id}")]
        public async Task<ActionResult<ReadRepairLogModel[]>> GetByMachineId(string id)
        {
            var repairLogs = await _repairLogService.GetRepairLogsByMachineIdAsync(id);
            return Ok(repairLogs);
        }

        // GET api/<RepairLogController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadRepairLogModel>> Get(string id)
        {
            var repairLogs = await _repairLogService.GetRepairLogByIdAsync(id);

            if (repairLogs == null)
            {
                return NotFound("No repairLog found for the given ID.");
            }

            return Ok(repairLogs);
        }
    }
}

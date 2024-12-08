using Augury.RepairTelemetryService.DomainServices;
using Augury.RepairTelemetryService.Models.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Augury.RepairTelemetryService.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MachineFeedController : ControllerBase
    {
        private readonly IMachineFeedService _machineFeedService;

        public MachineFeedController(IMachineFeedService machineFeedService) {
            _machineFeedService = machineFeedService;
        }
        [HttpGet()]
        public async Task<ActionResult<ReadMachineFeedModel[]>> GetAll()
        {
            var machineFeeds = await _machineFeedService.GetMachineFeedsAsync();
            return Ok(machineFeeds);
        }

        [HttpGet("machine/{id}")]
        public async Task<ActionResult<ReadMachineFeedModel[]>> GetByMachineId(string id)
        {
            var machineFeeds = await _machineFeedService.GetMachineFeedByMachineIdAsync(id);
            return Ok(machineFeeds);
        }

        // GET api/<MachineFeedController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadMachineFeedModel>> Get(string id)
        {
            var machineFeeds = await _machineFeedService.GetMachineFeedByIdAsync(id);

            if (machineFeeds == null)
            {
                return NotFound("No machineFeed found for the given ID.");
            }

            return Ok(machineFeeds);
        }
    }
}

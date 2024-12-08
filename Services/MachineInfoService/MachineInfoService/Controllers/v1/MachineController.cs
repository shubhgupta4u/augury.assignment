using Augury.MachineInfoService.DomainServices;
using Augury.MachineInfoService.Models.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Augury.MachineInfoService.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService machineService) {
            _machineService = machineService;
        }
        [HttpGet()]
        public async Task<ActionResult<ReadMachineModel[]>> GetAll()
        {
            var machines = await _machineService.GetMachinesAsync();
            return Ok(machines);
        }

        // GET api/<MachineController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadMachineModel>> Get(string id)
        {
            var machines = await _machineService.GetMachineByIdAsync(id);

            if (machines == null)
            {
                return NotFound("No machine found for the given ID.");
            }

            return Ok(machines);
        }
    }
}

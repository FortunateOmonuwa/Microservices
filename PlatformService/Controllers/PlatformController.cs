using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo repo;
        private readonly ICommandDataClient commandDataClient;
        private readonly IMapper mapper;
        public PlatformController(IPlatformRepo repo, IMapper mapper, ICommandDataClient commandDataClient)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.commandDataClient = commandDataClient;
        }
        [HttpGet]

        public ActionResult<PlatformReadDTO> GetPlatforms()
        {
            Console.WriteLine("Getting Platforms...");

            var platfrormItems = repo.GetAllPlatforms();
            return Ok(mapper.Map<IEnumerable<PlatformReadDTO>>(platfrormItems));
        }
        [HttpGet("{id}", Name ="GetPlatformbyId")]
        public IActionResult GetPlatformbyId( int id)
        {
            Console.WriteLine("Getting Platforms...");

            var platformItem = repo.GetPlatformById(id);
            if(platformItem != null)
            {
                Console.WriteLine("Platform Successfully found");
                return Ok(mapper.Map<PlatformReadDTO>(platformItem));
            }
            else
            {
                Console.WriteLine("Platform Not found");
                return NotFound();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlatform([FromBody] PlatformCreateDTO platformDTO)
        {
            var platformModel = mapper.Map<Platform>(platformDTO);

            repo.CreatePlatform(platformModel);
            var platfrom = mapper.Map<PlatformReadDTO>(platformModel);
            try
            {
                await commandDataClient.SendPlatformToCommand(platfrom);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Couldl not send platform to commmand client\n\n { ex.Message} ");
            }

            return CreatedAtRoute(nameof(GetPlatformbyId), new { platfrom.Id }, platfrom);

        }
    }
}

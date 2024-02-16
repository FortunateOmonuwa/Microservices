using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo repo;
        private readonly IMapper mapper;
        public PlatformController(IPlatformRepo repo, IMapper mapper)
        {
            this.mapper = mapper;
            this.repo = repo;
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
        public IActionResult CreatePlatform([FromBody] PlatformCreateDTO platformDTO)
        {
            var platformModel = mapper.Map<Platform>(platformDTO);

            repo.CreatePlatform(platformModel);
            var platfrom = mapper.Map<PlatformReadDTO>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformbyId), new { platfrom.Id }, platfrom);

        }
    }
}

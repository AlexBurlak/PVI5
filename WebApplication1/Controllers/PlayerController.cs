using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTO;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Requests;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ICategoryService _categoryService;
        private readonly List<Category> _categories = new List<Category>()
        {
            new Category() { Id = 0, Name = "UA", },
            new Category() { Id = 1, Name = "IT", },
        };
        private readonly List<Player> _players = new List<Player>()
        {
            new Player() { Id = 0, Email = "jordano@gmail.com", Name = "jordano", Password = "maikLobovski", Rank = "FM"},
            new Player() { Id = 1, Email = "bobby_stepanchuk@gmail.com", Name = "bobby2008", Password = "spongebob", Rank = "GM"},
        };

        public PlayerController(IPlayerService playerService, ICategoryService categoryService)
        {
            _playerService = playerService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var players = _players.ToList();
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var player = _players.FirstOrDefault(p => p.Id == id);
            if (player is null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]InsertRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _players.Add(new Player()
            {
                Id = _players.Count,
                Email = request.Email,
                Name = request.Name,
                Password = request.Password,
                Rank = request.Rank,
            });
            var player = _players.Last();
            return Created(nameof(Insert), player);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var player = _players.FirstOrDefault(p => p.Id == request.Id);
            if (player is null)
            {
                return NotFound();
            }
            player.Email = request.Email;
            player.Id = request.Id;
            player.Name = request.Name;
            player.Password = request.Password;
            player.Rank = request.Rank;
            _players.Remove(_players.FirstOrDefault(p => p.Id == request.Id));
            _players.Add(player);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var player = _players.FirstOrDefault(p => p.Id == id);
            if (player is null)
            {
                return NotFound();
            }
            _players.Remove(player);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProdeApi.Models;
using ProdeApi.Services;

namespace ProdeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        
        private readonly MatchService _service;

        public MatchController(MatchService matchService)
        {
            _service = matchService;
        }

        [HttpGet]
        public async Task<List<Match>> GetAllMatchPredictions()
        {
            return await _service.GetAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<Match> GetOneMatchPrediction(string id)
        {
            return await _service.GetOneAsync(id);
        }

        [HttpPost]
        public async Task<Match> AddMatchPrediction([FromBody] Match match)
        {
            await _service.CreateAsync(match);
            return match;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(string id, [FromBody] Match updatedMatch)
        {
            try
            {
                await _service.UpdateAsync(id, updatedMatch);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchPrediction(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

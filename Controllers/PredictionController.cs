using Microsoft.AspNetCore.Mvc;
using ProdeApi.Models;
using ProdeApi.Services;

namespace ProdeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        
        private readonly PredictionService _service;

        public PredictionController(PredictionService predictionService)
        {
            _service = predictionService;
        }

        [HttpGet]
        public async Task<List<UserPrediction>> GetAllMatchPredictions()
        {
            return await _service.GetAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<UserPrediction> GetOneMatchPrediction(string id)
        {
            return await _service.GetOneAsync(id);
        }

        [HttpPost]
        public async Task<UserPrediction> AddMatchPrediction([FromBody] UserPrediction userPrediction)
        {
            await _service.CreateAsync(userPrediction);
            return userPrediction;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(string id, [FromBody] UserPrediction updatedUserPrediction)
        {
            try
            {
                await _service.UpdateAsync(id, updatedUserPrediction);
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

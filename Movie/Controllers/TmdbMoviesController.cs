using Microsoft.AspNetCore.Mvc;
using Movie.Service;
namespace Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmdbMoviesController : ControllerBase
    {
        private readonly TmdbService _tmdbService;

        public TmdbMoviesController(TmdbService tmdbService)
        {

            _tmdbService = tmdbService;
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularFilms()
        {
            try
            {
                var films = await _tmdbService.GetPopularFilmsAsync();
                return Ok(films);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPopularFilms: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while fetching popular films." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTmdbFilmDetails(int id)
        {
            try
            {
                var film = await _tmdbService.GetFilmDetailsAsync(id);
                if (film == null)
                    return NotFound(new { Message = "Film not found." });

                return Ok(film);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetFilmDetails: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while fetching film details." });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchFilms([FromQuery] string query)
        {
            try
            {
                var films = await _tmdbService.SearchFilmsAsync(query);
                return Ok(films);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchFilms: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while searching for films." });
            }
        }

    }
}

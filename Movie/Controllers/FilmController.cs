using Microsoft.AspNetCore.Mvc;
using Movie.DTOs;
using Movie.Models;
using Movie.Service;
using Movie.Service.Interfaces;
using System.Security.Claims;

namespace Movie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmService _filmService;
        private readonly TmdbService _tmdbService;

        public FilmsController(IFilmService filmService, TmdbService tmdbService)
        {
            _filmService = filmService;

            _tmdbService = tmdbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilms()
        {
            try
            {
                var films = await _filmService.GetAllFilmsAsync();
                return Ok(films);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllFilms: Error - {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while retrieving films." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmById(int id)
        {
            try
            {
                var film = await _filmService.GetFilmByIdAsync(id);
                if (film == null) return NotFound(new { Message = "Film not found." });
                return Ok(film);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetFilmById: Error - {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while retrieving the film." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFilm([FromBody] FilmCreateDto filmDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if user is logged in
            if (!User.Identity.IsAuthenticated)
                return Unauthorized(new { Message = "You must be logged in to create a film." });

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "Unable to identify the logged-in user." });

            var film = new Film
            {
                Title = filmDto.Title,
                Genre = filmDto.Genre,
                ReleaseDate = filmDto.ReleaseDate,
                Rating = filmDto.Rating,
                Overview = filmDto.Overview,
                AppUserId = userId
            };

            try
            {
                var createdFilm = await _filmService.CreateFilmAsync(film);
                return CreatedAtAction(nameof(GetFilmById), new { id = createdFilm.Id }, createdFilm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateFilm: Error - {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while creating the film." });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilm(int id, [FromBody] FilmUpdateDto filmDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if user is logged in
            if (!User.Identity.IsAuthenticated)
                return Unauthorized(new { Message = "You must be logged in to update a film." });

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { Message = "Unable to identify the logged-in user." });

                var film = new Film
                {
                    Title = filmDto.Title,
                    Genre = filmDto.Genre,
                    ReleaseDate = filmDto.ReleaseDate,
                    Rating = filmDto.Rating,
                    Overview = filmDto.Overview,
                    AppUserId = userId
                };

                var updatedFilm = await _filmService.UpdateFilmAsync(id, film);
                if (updatedFilm == null)
                    return NotFound(new { Message = "Film not found for update." });

                return Ok(updatedFilm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateFilm: Error - {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while updating the film." });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized(new { Message = "You must be logged in to delete a film." });

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { Message = "Unable to identify the logged-in user." });

                var isDeleted = await _filmService.DeleteFilmAsync(id);

                if (!isDeleted)
                {
                    Console.WriteLine($"DeleteFilm: Film with ID {id} not found.");
                    return NotFound(new { Message = "Film not found for deletion." });
                }

                Console.WriteLine($"DeleteFilm: Successfully deleted film with ID {id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteFilm: Error - {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while deleting the film." });
            }
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchFilms(
            [FromQuery] string? title = null,
            [FromQuery] string? genre = null,
            [FromQuery] int? releaseYear = null,
            [FromQuery] double? minRating = null)
        {
            try
            {
                var films = await _filmService.SearchFilmsAsync(title, genre, releaseYear, minRating);
                return Ok(films);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SearchFilms: Error - {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while searching for films." });
            }
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetFilmDetails(int id)
        {
            try
            {
                var film = await _filmService.GetFilmDetailsAsync(id);
                if (film == null) return NotFound(new { Message = "Film details not found." });
                return Ok(film);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetFilmDetails: Error - {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while retrieving the film details." });
            }
        }


    }
}

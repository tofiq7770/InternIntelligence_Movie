namespace Movie.Service
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Movie.DAL;
    using Movie.Models;
    using Movie.Service.Interfaces;

    public class FilmService : IFilmService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FilmService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private bool IsUserLoggedIn()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated ?? false;
        }

        public async Task<IEnumerable<Film>> GetAllFilmsAsync()
        {
            return await _context.Films.ToListAsync();
        }

        public async Task<Film> GetFilmByIdAsync(int id)
        {
            return await _context.Films.FindAsync(id);
        }

        public async Task<Film> CreateFilmAsync(Film film)
        {
            if (!IsUserLoggedIn())
                throw new UnauthorizedAccessException("You must be logged in to create a film.");

            _context.Films.Add(film);
            await _context.SaveChangesAsync();
            return film;
        }

        public async Task<Film> UpdateFilmAsync(int id, Film film)
        {
            if (!IsUserLoggedIn())
                throw new UnauthorizedAccessException("You must be logged in to update a film.");

            var existingFilm = await _context.Films.FindAsync(id);
            if (existingFilm == null) return null;

            existingFilm.Title = film.Title;
            existingFilm.Genre = film.Genre;
            existingFilm.ReleaseDate = film.ReleaseDate;
            existingFilm.Rating = film.Rating;
            existingFilm.Overview = film.Overview;

            await _context.SaveChangesAsync();
            return existingFilm;
        }

        public async Task<bool> DeleteFilmAsync(int id)
        {
            if (!IsUserLoggedIn())
                throw new UnauthorizedAccessException("You must be logged in to delete a film.");

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                Console.WriteLine($"DeleteFilmAsync: Film with ID {id} not found.");
                return false;
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
            Console.WriteLine($"DeleteFilmAsync: Film with ID {id} successfully deleted.");
            return true;
        }

        public async Task<IEnumerable<Film>> SearchFilmsAsync(string? title, string? genre, int? releaseYear, double? minRating)
        {
            var query = _context.Films.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(f => EF.Functions.Like(f.Title, $"%{title}%"));

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(f => EF.Functions.Like(f.Genre, genre));

            if (releaseYear.HasValue)
                query = query.Where(f => f.ReleaseDate.Year == releaseYear.Value);

            if (minRating.HasValue)
                query = query.Where(f => f.Rating >= minRating.Value);

            return await query.ToListAsync();
        }

        public async Task<Film> GetFilmDetailsAsync(int id)
        {
            return await _context.Films.FindAsync(id);
        }
    }
}

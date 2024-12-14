using Movie.Models;

namespace Movie.Service.Interfaces
{
    public interface IFilmService
    {
        Task<IEnumerable<Film>> GetAllFilmsAsync();
        Task<Film> GetFilmByIdAsync(int id);
        Task<Film> CreateFilmAsync(Film film);
        Task<Film> UpdateFilmAsync(int id, Film film);
        Task<bool> DeleteFilmAsync(int id);
        Task<IEnumerable<Film>> SearchFilmsAsync(string title, string genre, int? releaseYear, double? minRating);
        Task<Film> GetFilmDetailsAsync(int id);
    }


}

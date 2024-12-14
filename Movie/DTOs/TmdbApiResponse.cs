namespace Movie.DTOs
{
    public class TmdbApiResponse
    {
        public IEnumerable<FilmDto> Results { get; set; }
    }

}
